using SFA.DAS.Azure.ServiceBus.Tools.DLQConsole;

Console.Write("Enter Azure Service Bus connection string: ");
var connectionString = Console.ReadLine();

Console.Write("Enter Azure Topic name: ");
var topicName = Console.ReadLine();

Console.Write("Enter Azure Topic Subscription name: ");
var subscriptionName = Console.ReadLine();

Console.WriteLine("Moving messages from dead letter queue to topic queue. Please wait...");

await DeadLetterRescheduler.RequeueMessages(
    connectionString,
    topicName,
    subscriptionName
);

Console.WriteLine("All dead letter messages have been requeued.");

