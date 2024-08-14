using Azure.Messaging.ServiceBus;
using static SFA.DAS.Azure.ServiceBus.Tools.DLQConsole.Extensions.ConsoleExtensions;

namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole;

public class DeadLetterRescheduler
{
    private readonly string _topicName;
    private readonly string _subscriptionName;
    private readonly ServiceBusClient _client;
    private readonly ServiceBusProcessor _deadLetterProcessor;

    public DeadLetterRescheduler(string connectionString, string topicName, string subscriptionName)
    {
        _topicName = topicName;
        _subscriptionName = subscriptionName;
        _client = new ServiceBusClient(connectionString);

        _deadLetterProcessor = _client.CreateProcessor(
            topicName: topicName,
            subscriptionName: subscriptionName,
            new ServiceBusProcessorOptions
            {
                SubQueue = SubQueue.DeadLetter,
                MaxConcurrentCalls = 2
            }
        );
    }

    public async Task RequeueMessages()
    {
        try
        {
            await using var sender = _client.CreateSender(_topicName);

            await PickUpAndFixDeadletters(sender);
        }
        catch (Exception e)
        {
            WriteError(e.ToString());
            throw;
        }
        finally
        {
            await _deadLetterProcessor.DisposeAsync();
            await _client.DisposeAsync();
        }
    }

    private async Task PickUpAndFixDeadletters(ServiceBusSender resubmitSender)
    {
        var messageCount = 0;

        _deadLetterProcessor.ProcessMessageAsync += async args =>
        {
            var resubmitMessage = new ServiceBusMessage(args.Message);
            await resubmitSender.SendMessageAsync(resubmitMessage);
            await args.CompleteMessageAsync(args.Message);
            messageCount++;
        };

        _deadLetterProcessor.ProcessErrorAsync += args =>
        {
            WriteError($"Exception: {args.Exception.Message} {args.EntityPath}");
            WriteError(args.Exception.ToString());

            return Task.CompletedTask;
        };

        await _deadLetterProcessor.StartProcessingAsync();

        Console.WriteLine($"Beginning processing dead-letter messages from subscription '{_subscriptionName}'.");
        Console.WriteLine("Wait for a minute and then press any key to end the processing");
        Console.ReadKey();

        Console.WriteLine($"Successfully processed {messageCount} dead-letter messages.");
        Console.WriteLine("Stopping the receiver...");

        await _deadLetterProcessor.StopProcessingAsync();

        Console.WriteLine("Message processing has stopped.");
    }
}