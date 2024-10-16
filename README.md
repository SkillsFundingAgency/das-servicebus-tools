# das-servicebus-tools

# The SFA.DAS.ServiceBus.Tools.Functions app

The SFA.DAS.ServiceBus.Tools.Function application is a simple Azure hosted Function which allows publishing specific messages via HttpTriggers. Execution via the Azure Portal is recommended for simplicity but you will need to ensure a CORS exeception has been added for 'https://portal.azure.com' beforehand.

The messages which can be published are:

* DraftExpireAccountFunds
* DraftExpireFunds
* ExpireAccountFunds
* ExpireFunds
* ImportAccountLevyDeclarations
* ImportPayments
* ProcessPeriodEndPayments

All of the HttpTrigger endpoint verbs are POST. The shape of the messges are as shown below:

### DraftExpireAccountFunds

```javascript
{
    "AccountId": "00000",
    "DateTo": "2024-01-01"
}
```
---
### DraftExpireFunds

```javascript
{
    "DateTo": "2024-01-01"
}
```
---
### ExpireAccountFunds

```javascript
{
    "AccountId": "00000",
}
```
---
### ExpireFunds
```javascript
{ 

}
```
---
### ImportAccountLevyDeclarations

```javascript
{
    "AccountId": "00000",
    "PayeRef": "ABC/123245"
}
```
---
### ImportPayments
```javascript
{

}
```
---
### ImportAccountPayments

```javascript
{
    "AccountId": "00000",
    "PeriodEndRef": "2425-R01"
}
```
---
### ProcessPeriodEndPayments

```javascript
{
    "PeriodEndRef": "2324-R10",
    "BatchNumber": "1"
}
```

---



# Running the dead letter message requeue app

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


