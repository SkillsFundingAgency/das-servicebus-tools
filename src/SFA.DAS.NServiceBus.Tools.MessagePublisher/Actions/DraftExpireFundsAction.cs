using System;
using NServiceBus;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.Configuration.NLog;
using SFA.DAS.NServiceBus.Configuration.StructureMap;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;
using StructureMap;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions
{
    public class DraftExpireFundsAction
    {
        public static void Execute(DraftExpireFundsVerb verb, IContainer container)
        {
            WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);

            var endpointConfiguration = new EndpointConfiguration(verb.EndpointName)
                .UseAzureServiceBusTransport<DraftExpireFundsCommand>(verb.ServiceBusConnectionString, verb.IsDevelopmentEnvironment, verb.EndpointName)
                .UseLicense(verb.License.HtmlDecode())
                .UseNewtonsoftJsonSerializer()
                .UseNLogFactory()
                .UseSendOnly()
                .UseStructureMapBuilder(container);

            var endpoint = Endpoint.Start(endpointConfiguration).Result;

            WriteToConsole("Debug information below:", ConsoleColours.Debug);
            WriteToConsole($"Environment: {verb.Environment}", ConsoleColours.Debug);
            WriteToConsole($"Endpoint: {verb.EndpointName}", ConsoleColours.Debug);
            WriteToConsole($"Service Bus Connection: {verb.ServiceBusConnectionString}", ConsoleColours.Debug);
            WriteToConsole($"License: {verb.License}", ConsoleColours.Debug);
            WriteToConsole($"Is Development: {verb.IsDevelopmentEnvironment}", ConsoleColours.Debug);

            SendExpireFundsCommand(endpoint);
        }
        private static void SendExpireFundsCommand(IMessageSession endpoint)
        {
            try
            {
                WriteToConsole($"Sending Message", ConsoleColours.Debug);
                
                endpoint.Send(new ExpireFundsCommand()).GetAwaiter().GetResult();

                WriteToConsole("Message sent successfully", ConsoleColours.Success);
            }
            catch (Exception e)
            {
                WriteToConsole("Failed to send command: " + e, ConsoleColours.Error);
                throw;
            }
        }

        private static void WriteToConsole(string text, ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }
    }
}