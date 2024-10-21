using static SFA.DAS.Azure.ServiceBus.Tools.DLQConsole.Extensions.ConsoleExtensions;

namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Apprenticeship Service - Dead-Letter Re-Enqueue Application.");
        Console.WriteLine();
        
        var connectionString = GetInput("Enter Azure Service Bus connection string: ");
        var topicName = GetInput("Enter Azure Topic name: ");
        var subscriptionName = GetInput("Enter Azure Topic Subscription name: ");

        var scheduler = new DeadLetterRescheduler(
            connectionString,
            topicName,
            subscriptionName
        );

        await scheduler.RequeueMessages();
    }
}