# das-servicebus-tools

## Running the dead letter message requeue app

DLQConsole application will read all the messages in a dead letter queue and add them back onto the topic queue they where dead lettered from. 

__NOTE:__ The requeue of messages will send the requeue queue message to every subscription even if only one subscription has failed to process the message so it is worth making sure either subscribers can handle multiple duplicate message publishing correcly or there is only one subscription to the topic where the message is being requeued.

### Starting the console application
To run the application you need to start the exe file (preferably from command line if you want the messages to persist on screen).

### Entering the Azure queue details
The dead letter requeue app needs the following details to work correctly:

- Connection string to the azure service bus the dead letter queue exists on
- The topic name of the dead letter queue
- The topic subscription name as the dead letter queue is per subscription

When running the application you will be prompted to enter the above information in the same order.

__NOTE:__ Currently the application has only been tested with connection strings that have read, write and manage permissions.

Once you have entered all the above information when prompted the application will look at each message in the dead letter queue for that topic and subscription and attemp to requeue onto the same topic. You will see console messages for each message processed and will get a conformation message when all processing is completed. All of the messages are fairly obvious so there should be no need to list them here.

### Error handling

Currently the dead letter requeue app has no error handling and this will follow once clearer requirements are worked out. 



