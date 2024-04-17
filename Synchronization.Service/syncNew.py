import pika

credentials = pika.PlainCredentials('username', 'password')
parameters = pika.ConnectionParameters(credentials=credentials)


def on_connected(connection):
    """Called when we are fully connected to RabbitMQ"""
    # Open a channel
    connection.channel(on_open_callback=on_channel_open)


def on_channel_open(new_channel):
    """Called when our channel has opened"""
    global channel
    channel = new_channel
    channel.queue_declare(queue="test", durable=True, exclusive=False, auto_delete=False, callback=on_queue_declared)


def on_queue_declared(frame):
    """Called when RabbitMQ has told us our Queue has been declared, frame is the response from RabbitMQ"""
    channel.basic_consume('test', handle_delivery)


def handle_delivery(channel, method, header, body):
    """Called when we receive a message from RabbitMQ"""
    print(body)


def on_close(connection, exception):
    # Invoked when the connection is closed
    connection.ioloop.stop()


parameters = pika.ConnectionParameters()
connection = pika.SelectConnection(on_open_callback=on_connected, on_close_callback=on_close)


def on_message(channel, method_frame, header_frame, body):
    print(method_frame.delivery_tag)
    print(body)
    print()
    channel.basic_ack(delivery_tag=method_frame.delivery_tag)


connection = pika.BlockingConnection()
channel = connection.channel()
channel.basic_consume('test', on_message)
try:
    channel.start_consuming()
except KeyboardInterrupt:
    channel.stop_consuming()
connection.close()



try:

    connection.ioloop.start()
except KeyboardInterrupt:
   
    connection.close()
    
    connection.ioloop.start()