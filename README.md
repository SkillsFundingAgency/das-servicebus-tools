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


## Running the NServiceBus message publisher app

The NServiceBus message publisher is a simple command line tool to create and add a message onto a NServiceBus message queue. The command line tool is designed to work with command line parameters rather than being console based. The command based tool parameters work using the following format:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe ** [Verb] [Options]** 

You can view any help information at any time by using the * help * verb

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe help

You can get help on a specific command by using the * --help * option for that command

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe [Verb] --help


Below explains how to add messages to a queue of a given type.

__NOTE:__ You can use the short (-a) or long (--account) option tags to define parameters being sent to the application. You shoud then see output of the result of your request. Errors should show in red and success messages in green. There is also debug information shown in cyan.


### Adding an Process period end payments message [processperiodendpayments verb]

To add a message to a NServiceBus queue which kicks off payment importing for all accounts for a specified period end you need to use the 'processperiodendpayments' verb with the following options:

- [-p, --period] Period end (i.e. 1718-R01)
- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe processperiodendpayments -p 1718-R01 -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]

### Adding an import payments message [importpayments verb]

To add a message to a NServiceBus queue which kicks off payment importing for a specific account and period end you need to use the 'importpayments' verb with the following options:

- [-a, --account] Account ID (Number) (i.e. 12)
- [-p, --period] Period end (i.e. 1718-R01)
- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe importpayments -a 21 -p 1718-R01 -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]
  

### Adding an import levy declaration message [importdeclarations verb]

To add a message to a NServiceBus queue which kicks off levy declarations importing for a specific account and PAYE scheme you need to use the 'importdeclarations' verb with the following options:

- [-a, --account] Account ID (Number) (i.e. 12)
- [-p, --payeref] PAYE Scheme (i.e. AAA/00100AA)
- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe importdeclarations -a 21 -p AAA/00200AA -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]


### Adding an expire funds message [expirefunds verb]

To add a message to a NServiceBus queue which kicks off expiring funds for all accounts you need to use the 'expirefunds' verb with the following options:

- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe expirefunds -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]
  

### Adding an expire account funds message [expireaccountfunds verb]

To add a message to a NServiceBus queue which kicks off expiring funds for a specific account you need to use the 'expireaccountfunds' verb with the following options:

- [-a, --account] Account ID (Number) (i.e. 12)
- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe expireaccountfunds -a 21 -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]
  

### Adding an draft expire funds message [draftexpirefunds verb]

To add a message to a NServiceBus queue which kicks off expiring funds for a specific account you need to use the 'draftexpireaccountfunds' verb with the following options:

- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe draftexpirefunds -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]


### Adding an draft expire account funds message [draftexpireaccountfunds verb]

To add a message to a NServiceBus queue which kicks off expiring funds for a specific account you need to use the 'draftexpireaccountfunds' verb with the following options:

- [-a, --account] Account ID (Number) (i.e. 12)
- [-e, --environment] The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD)
- [-n, --endpoint] The NServiceBus endpoint the message will be published to
- [-c, --connection] The connection string for the target NServiceBus instance
- [-l, --license] The license string that contains the NServiceBus license.

Below is an example of usage:

> SFA.DAS.NServiceBus.Tools.MessagePublisher.exe draftexpireaccountfunds -a 21 -e TEST -n "SFA.DAS.EmployerFinance.MessageHandlers" -c [connection string] -l [license text]
