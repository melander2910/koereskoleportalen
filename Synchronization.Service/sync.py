import pika
import json
from pymongo import MongoClient

# MongoDB connection setup
client = MongoClient('mongodb://localhost:27017/')
db = client.my_database  # Use your actual database name
collection = db.my_collection  # Use your actual collection name

# RabbitMQ setup
credentials = pika.PlainCredentials('Admin', 'Admin2024')
parameters = pika.ConnectionParameters(host='localhost',
                                       port=5672,
                                       virtual_host='/',
                                       credentials=credentials)
connection = pika.BlockingConnection(parameters)
channel = connection.channel()

# Declare the main exchange, queue, and setup for dead lettering
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

        # Assume update_data contains a unique identifier and fields to update
        filter_criteria = {'_id': update_data['_id']}  # or any other unique field
        update_operation = {'$set': update_data}

        # Perform the update
        result = collection.update_one(filter_criteria, update_operation)
        if result.modified_count > 0:
            print("Document updated successfully.")
        else:
            print("No document matches the given criteria.")

        ch.basic_ack(delivery_tag=method.delivery_tag)
    except json.JSONDecodeError:
        print("Failed to decode JSON.")
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
    except Exception as error:
        print(f"Error processing message: {error}")
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)


# Start consuming messages with the JSON handler
channel.basic_consume(queue='my_queue', on_message_callback=on_message, auto_ack=False)
print('Starting to consume')
channel.start_consuming()
