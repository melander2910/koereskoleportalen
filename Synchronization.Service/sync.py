import pika
import json
from pymongo import MongoClient
from bson import ObjectId


# MongoDB setup
client = MongoClient('mongodb://localhost:27017/')
db_name='koereskoleportalen-portal'
db_collection='companies'
db = client[db_name] # Update with your database name
collection = db[db_collection]  # Update with your collection name


# RabbitMQ setup
credentials = pika.PlainCredentials('Admin', 'Admin2024')
parameters = pika.ConnectionParameters(host='localhost',
                                       port=5672,
                                       virtual_host='/',
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

def on_message(ch, method, properties, body):
    try:
        update_data = json.loads(body.decode())
        print(f"Received update request: {update_data}")

        # Save received JSON to a file
        with open('received_updates.json', 'a') as f:
            json.dump(update_data, f)
            f.write('\n')

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

    except json.JSONDecodeError:
        print("Failed to decode JSON.")
        # Reject the message and send it to the dead letter queue
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)

    except Exception as error:
        print(f"Error processing message: {error}")
        # Reject the message and send it to the dead letter queue
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)


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
