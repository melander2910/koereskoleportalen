import pika

def publish_message(queue_name, message):
    # Set up connection parameters with authentication
    credentials = pika.PlainCredentials('guest', 'guest')
    parameters = pika.ConnectionParameters('localhost',
                                           5672,  # Default port for RabbitMQ
                                           '/',    # Default virtual host
                                           credentials)
    
    # Establish a connection with RabbitMQ server
    connection = pika.BlockingConnection(parameters)
    channel = connection.channel()

    # Declare the queue to ensure it exists
    channel.queue_declare(queue=queue_name, durable=True)

    # Publish the message to the queue
    channel.basic_publish(exchange='',
                          routing_key=queue_name,
                          body=message,
                          properties=pika.BasicProperties(
                              delivery_mode=2,  # make message persistent
                          ))

    print(f"Sent '{message}' to queue '{queue_name}'")
    
    # Close the connection
    connection.close()

# Example usage
if __name__ == "__main__":
    queue_name = 'hello'
    message = 'Hello World!'
    publish_message(queue_name, message)
