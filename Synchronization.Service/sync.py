import pika
import json

def on_message_received(ch, method, properties, body):
    try:
        message = json.loads(body.decode('utf-8'))
        print("Received message:", message)
        # Processing logic here
        ch.basic_ack(delivery_tag=method.delivery_tag)
    except json.JSONDecodeError as e:
        print(f"Failed to decode JSON: {e}")
        # Decide whether to negatively acknowledge or reject the message
        # ch.basic_nack(delivery_tag=method.delivery_tag)
    except Exception as e:
        print(f"An error occurred: {e}")
        # Similar decision needs to be made here

# Connection parameters with authentication
credentials = pika.PlainCredentials('guest', 'guest')
parameters = pika.ConnectionParameters('localhost',
                                       5672,
                                       '/',  # specific vhost
                                       credentials)

connection = pika.BlockingConnection(parameters)
channel = connection.channel()

queue_name = 'queue_name'
channel.queue_declare(queue=queue_name, durable=True)

channel.basic_consume(
    queue=queue_name,
    on_message_callback=on_message_received
)

print('Waiting for messages. To exit press CTRL+C')
channel.start_consuming()
print()
