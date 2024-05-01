import pika
import json
import time
import pybreaker
from pymongo import MongoClient, errors
from bson import ObjectId
from pymongo.errors import ConnectionFailure, OperationFailure


def load_config():
    with open('config.json', 'r') as file:
        return json.load(file)

config = load_config()

# Accessing RabbitMQ configuration
RABBITMQ_USER = config['rabbitmq']['RABBITMQ_USER']
RABBITMQ_HOST = config['rabbitmq']['RABBITMQ_HOST']
RABBITMQ_PORT = config['rabbitmq']['RABBITMQ_PORT']
RABBITMQ_VHOST = config['rabbitmq']['RABBITMQ_VHOST']
RABBITMQ_PASSWORD = config['rabbitmq']['RABBITMQ_PASSWORD']

# Constructing MongoDB URI and accessing other MongoDB configuration
MONGO_HOST = config['mongo']['MONGO_HOST']
MONGO_PORT = config['mongo']['MONGO_PORT']
MONGO_DB_NAME = config['mongo']['MONGO_DB_NAME']
MONGO_DB_COLLECTION = config['mongo']['MONGO_DB_COLLECTION']
MONGODB_URI = f"mongodb://{MONGO_HOST}:{MONGO_PORT}"




def connect_with_retry(client, uri, max_attempts=5):
    attempt = 0
    while attempt < max_attempts:
        try:
            print(f"Checking MongoDB connection at {uri}")
            # The ismaster command is cheap and does not require auth and is now replaced by 'ping'.
            client.admin.command('ping')
            print("Connection successful.")
            return True
        except ConnectionFailure:
            print("Connection failed. Attempting to reconnect...")
            attempt += 1
            sleep_time = 2 ** attempt  # Exponential backoff
            print(f"Retrying in {sleep_time} seconds...")
            time.sleep(sleep_time)
            try:
                # Reinitialize the MongoClient, which may help in reconnecting in certain scenarios.
                client = MongoClient(uri)
            except ConnectionFailure:
                continue  # Continue the loop if reinitialization fails
    raise Exception("Database connection failed after several attempts")


# MongoDB setup
client = MongoClient(MONGODB_URI)
try:
    connect_with_retry(client, MONGODB_URI)
except Exception as e:
    print(str(e))
    exit(1)  # Exit the script or handle the failure appropriately
db_name= MONGO_DB_NAME
db_collection= MONGO_DB_COLLECTION
db = client[db_name] # Update with your database name
collection = db[db_collection]  # Update with your collection name
mongo_circuit_breaker = pybreaker.CircuitBreaker(fail_max=3, reset_timeout=30)


# RabbitMQ setup
credentials = pika.PlainCredentials(RABBITMQ_USER, RABBITMQ_PASSWORD)
parameters = pika.ConnectionParameters(host=RABBITMQ_HOST,
                                       port=RABBITMQ_PORT,
                                       virtual_host=RABBITMQ_VHOST,
                                       credentials=credentials)
connection = pika.BlockingConnection(parameters)
channel = connection.channel()

# Declarring the main exchange, queue, and setup for dead lettering
channel.exchange_declare(exchange='main_exchange', exchange_type='direct', durable=True)
channel.exchange_declare(exchange='dead_letter_exchange', exchange_type='direct', durable=True)
channel.queue_declare(queue='my_queue', arguments={
    'x-dead-letter-exchange': 'dead_letter_exchange',
    'x-dead-letter-routing-key': 'dead'
})
channel.queue_declare(queue='dead_letter_queue')
channel.queue_bind(queue='my_queue', exchange='main_exchange', routing_key='main')
channel.queue_bind(queue='dead_letter_queue', exchange='dead_letter_exchange', routing_key='dead')


def is_mongodb_connected(client):
    """ Check if MongoDB is connected """
    try:
        # The ismaster command is cheap and does not require auth.
        client.admin.command('ismaster')
        return True
    except errors.ConnectionFailure:
        print("MongoDB not available")
        return False

def on_message(ch, method, properties, body):
    try:
        if not is_mongodb_connected(client):
            print("MongoDB connection failed. Sending message to dead letter queue.")
            # Nack the message and send it to the dead letter queue
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
            return

        update_data = json.loads(body.decode())
        print(f"Received update request: {update_data}")

        # Save received JSON to a file
        with open('received_updates.json', 'a') as f:
            json.dump(update_data, f)
            f.write('\n')  # Write a newline to separate JSON entries

        if '_id' in update_data and '$oid' in update_data['_id']:
            update_data['_id'] = ObjectId(update_data['_id']['$oid'])

        filter_criteria = {'_id': update_data['_id']}
        update_operation = {'$set': update_data}

        # Perform the update
        result = collection.update_one(filter_criteria, update_operation)
        if result.modified_count > 0:
            print("Document updated successfully.")
        else:
            print("No document matches the given criteria.")

        # Acknowledge the message as successfully processed
        ch.basic_ack(delivery_tag=method.delivery_tag)

    except json.JSONDecodeError as e:
        print("JSON Decode Error, will not retry.")
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
    except Exception as e:
        print("Processing error, message will be requeued.")
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=True)


# Start consuming messages with the JSON handler
channel.basic_consume(queue='my_queue', on_message_callback=on_message, auto_ack=False)
print('Starting to consume')
channel.start_consuming()


#If the message is a json file
def update_database_from_json(json_data, collection):
    for item in json_data:
        filter_criteria = {'_id': item['_id']}
        update_operation = {'$set': item}
        result = collection.update_one(filter_criteria, update_operation, upsert=True)
        if result.matched_count:
            print(f"Updated document with _id: {item['_id']}")
        else:
            print(f"Inserted new document with _id: {item['_id']}")
