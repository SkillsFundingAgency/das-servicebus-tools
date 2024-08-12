using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole;

public static class DeadLetterRescheduler
{
    public static async Task RequeueMessages(string connectionString, string topicName, string subscriptionName)
    {
        await using var client = new ServiceBusClient(connectionString, new DefaultAzureCredential());

        await using var sender = client.CreateSender(topicName);

        await PickUpAndFixDeadletters(client, topicName, subscriptionName, sender);
    }

    private static async Task PickUpAndFixDeadletters(ServiceBusClient client, string topicName, string subscriptionName, ServiceBusSender resubmitSender)
    {
        await using var dlqProcessor = client.CreateProcessor(
            topicName: topicName,
            subscriptionName: subscriptionName,
            new ServiceBusProcessorOptions { SubQueue = SubQueue.DeadLetter }
        );

        dlqProcessor.ProcessMessageAsync += async args => { await ProcessMessage(resubmitSender, args); };

        dlqProcessor.ProcessErrorAsync += LogMessageHandlerException;

        _ = dlqProcessor.StartProcessingAsync();
    }

    private static async Task ProcessMessage(ServiceBusSender resubmitSender, ProcessMessageEventArgs args)
    {
        var resubmitMessage = new ServiceBusMessage(args.Message);

        Console.WriteLine($"Moving message with ID: {resubmitMessage.MessageId}.");

        await resubmitSender.SendMessageAsync(resubmitMessage);

        await args.CompleteMessageAsync(args.Message);

        Console.WriteLine($"Moving message with ID: {resubmitMessage.MessageId} completed.");
    }

    private static Task LogMessageHandlerException(ProcessErrorEventArgs e)
    {
        Console.WriteLine($"Exception: \"{e.Exception.Message}\" {e.EntityPath}");
        return Task.CompletedTask;
    }
}