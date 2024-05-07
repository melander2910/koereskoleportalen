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
MONGO_DS_COLLECTION = config['mongo']['MONGO_DS_COLLECTION']
MONGO_ORG_COLLECTION = config['mongo']['MONGO_ORG_COLLECTION']
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
    exit(1) 
db = client[MONGO_DB_NAME]
ds_collection = db[MONGO_DS_COLLECTION]
org_collection = db[MONGO_ORG_COLLECTION]
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
channel.queue_declare(queue='organisations_queue', arguments={'x-dead-letter-exchange': 'dead_letter_exchange','x-dead-letter-routing-key': 'dead'})
channel.queue_declare(queue='drivingschools_queue', arguments={'x-dead-letter-exchange': 'dead_letter_exchange','x-dead-letter-routing-key': 'dead'})
channel.queue_declare(queue='dead_letter_queue')
channel.queue_bind(queue='organisations_queue', exchange='main_exchange', routing_key='organisations')
channel.queue_bind(queue='drivingschools_queue', exchange='Contracts:ProductionUnitUpdatedEvent', routing_key='drivingschools')
channel.queue_bind(queue='dead_letter_queue', exchange='dead_letter_exchange', routing_key='dead')


def on_message_organisations(ch, method, properties, body):
    
    if not connect_with_retry(client, MONGODB_URI):
        print("Failed to connect to MongoDB. Exiting...")
        exit(1)
    else:
        
        try:
            update_data = json.loads(body.decode())

            # Assuming the message contains 'OrganisationNumber' as the identifier
            if 'OrganisationNumber' not in update_data:
                print("OrganisationNumber missing in data")
                ch.basic_ack(delivery_tag=method.delivery_tag)  # Acknowledge message as processed
                return

            filter_criteria = {'OrganisationNumber': update_data['OrganisationNumber']}
            update_operation = {'$set': update_data}

            # Perform the update in MongoDB
            result = org_collection.update_one(filter_criteria, update_operation)
            if result.modified_count > 0:
                print("Organisation document updated successfully.")
            else:
                print("No organisation matches the given criteria.")

            ch.basic_ack(delivery_tag=method.delivery_tag)

        except json.JSONDecodeError as e:
            print("JSON Decode Error, will not retry.")
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
        except Exception as e:
            print("Processing error, message will be requeued.")
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=True)

    
    
def on_message_drivingschools(ch, method, properties, body):
    
    if not connect_with_retry(client, MONGODB_URI):
        print("Failed to connect to MongoDB. Exiting...")
        exit(1)
    else:
        try:
            update_data = json.loads(body.decode())
            print(update_data)

            # Assuming the message contains 'ProductionUnitNumber' as the identifier
            if 'ProductionUnitNumber' not in update_data:
                print("ProductionUnitNumber missing in data")
                ch.basic_ack(delivery_tag=method.delivery_tag)  # Acknowledge message as processed
                return

            
            filter_criteria = {'ProductionUnitNumber': update_data['ProductionUnitNumber']}
            update_operation = {'$set': update_data}

            # Perform the update in MongoDB
            result = ds_collection.update_one(filter_criteria, update_operation)
            if result.modified_count > 0:
                print("Driving school document updated successfully.")
            else:
                print("No driving school matches the given criteria.")

            ch.basic_ack(delivery_tag=method.delivery_tag)

        except json.JSONDecodeError as e:
            print("JSON Decode Error, will not retry.")
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
        except Exception as e:
            print("Processing error, message will be requeued.")
            ch.basic_nack(delivery_tag=method.delivery_tag, requeue=True)
    
    
    
def start_consuming_messages():
    connection = pika.BlockingConnection(
        pika.ConnectionParameters(host=RABBITMQ_HOST ,port=RABBITMQ_PORT, virtual_host=RABBITMQ_VHOST, credentials=credentials)
    )
    channel = connection.channel()

    # Organisations queue consumer
    channel.basic_consume(
        queue='organisations_queue',
        on_message_callback=on_message_organisations,
        auto_ack=True
    )

    # Driving schools queue consumer
    channel.basic_consume(
        queue='drivingschools_queue',
        on_message_callback=on_message_drivingschools,
        auto_ack=True
    )

    print("Starting to consume messages from queues")
    channel.start_consuming()

start_consuming_messages()