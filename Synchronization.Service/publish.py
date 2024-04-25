import pika
import json

# Setup credentials and connection parameters
credentials = pika.PlainCredentials('Admin', 'Admin2024')
parameters = pika.ConnectionParameters(host='localhost',
                                       port=5672,
                                       virtual_host='/',
                                       credentials=credentials)

# Connect to RabbitMQ
connection = pika.BlockingConnection(parameters)
channel = connection.channel()

# Ensure the queue exists
channel.queue_declare(queue='my_new_queue')

# Prepare a JSON message
message = {
  "_id": {
    "$oid": "661f78b99d44989496f80c50"
  },
  "DrivingSchools": [],
  "OrganisationNumber": 35091971,
  "Name": "Morten Lützhøft",
  "Address": "Søndergårdsvej 19",
  "ZipCode": 2870,
  "City": "GENTOFTEEEEEEEE",
  "IndustryCode": 0,
  "Email":"",
  "PhoneNumber":"",
}

# Convert the message to a JSON formatted string
json_message = json.dumps(message)

# Publish the message
channel.basic_publish(exchange='',
                      routing_key='my_queue',
                      body=json_message)

print(" [x] Sent 'JSON Message'")

# Close the connection
connection.close()
