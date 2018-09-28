using System;

namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter Azure Service Bus connection string: ");
            var connectionString = Console.ReadLine();

            Console.Write("Enter Azure Topic name: ");
            var topicName = Console.ReadLine();

            Console.Write("Enter Azure Topic Subscription name: ");
            var subscriptionName = Console.ReadLine();

            Console.WriteLine("Moving messages from dead letter queue to topic queue. Please wait...");

            DeadLetterRescheduler.RequeueMessages(connectionString, topicName, subscriptionName).Wait();

            Console.WriteLine("All dead letter messages have been requeued.");
        }
    }
}
