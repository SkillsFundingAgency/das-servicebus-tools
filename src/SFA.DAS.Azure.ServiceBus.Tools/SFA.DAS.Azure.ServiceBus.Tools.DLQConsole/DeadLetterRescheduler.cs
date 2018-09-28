using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole
{
    public static class DeadLetterRescheduler
    {
        public static async Task RequeueMessages(string connectionString, string topicName, string subscriptionName)
        {
            var dlqClient = CreateDeadLetterQueueClient(connectionString, topicName, subscriptionName);
            var topicClient = TopicClient.CreateFromConnectionString(connectionString, topicName);

            var hasMessages = true;

            while (hasMessages)
            {
                var message = await dlqClient.ReceiveAsync(TimeSpan.FromSeconds(2));

                if (message == null)
                {
                    hasMessages = false;
                    continue;
                }

                Console.WriteLine($"Moving message with ID: {message.MessageId}.");

                await topicClient.SendAsync(new BrokeredMessage(message.GetBody<Stream>()));

                await message.CompleteAsync();

                Console.WriteLine($"Moving message with ID: {message.MessageId} Completed.");
            }

            await dlqClient.CloseAsync();
        }


        private static MessageReceiver CreateDeadLetterQueueClient(string connectionString, string topicName, string subscriptionName)
        {
            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var factory = MessagingFactory.CreateFromConnectionString(builder.ToString());
            var deadLetterQueuePath = SubscriptionClient.FormatDeadLetterPath(topicName, subscriptionName);

            return factory.CreateMessageReceiver(deadLetterQueuePath);
        }
    }
}
