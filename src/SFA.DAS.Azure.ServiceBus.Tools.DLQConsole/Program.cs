using SFA.DAS.Azure.ServiceBus.Tools.DLQConsole;

public class Program
{
    public static async Task Main(string[] args)
    {
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

    private static string GetInput(string message)
    {
        string? input;

        do
        {
            Console.WriteLine(message);
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be null");
            }
            
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }
}