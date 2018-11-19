﻿using System;
using NServiceBus;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.NLog;
using SFA.DAS.NServiceBus.StructureMap;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;
using StructureMap;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions
{
    public class ImportAccountLevyDeclarationsAction
    {
        public static void Execute(ImportAccountLevyDeclarationsVerb verb, IContainer container)
        {
            WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);

            var endpointConfiguration = new EndpointConfiguration(verb.EndpointName)
                .UseAzureServiceBusTransport<ImportAccountLevyDeclarationsCommand>(() => verb.ServiceBusConnectionString, verb.IsDevelopmentEnvironment, verb.EndpointName)
                .UseLicense(verb.License.HtmlDecode())
                .UseNewtonsoftJsonSerializer()
                .UseNLogFactory()
                .UseSendOnly()
                .UseStructureMapBuilder(container);

            var endpoint = Endpoint.Start(endpointConfiguration).Result;

            WriteToConsole("Debug information below:", ConsoleColours.Debug);
            WriteToConsole($"Account ID: {verb.AccountId}", ConsoleColours.Debug);
            WriteToConsole($"PAYE Scheme: {verb.PayeRef}", ConsoleColours.Debug);
            WriteToConsole($"Environment: {verb.Environment}", ConsoleColours.Debug);
            WriteToConsole($"Endpoint: {verb.EndpointName}", ConsoleColours.Debug);
            WriteToConsole($"Service Bus Connection: {verb.ServiceBusConnectionString}", ConsoleColours.Debug);
            WriteToConsole($"License: {verb.License}", ConsoleColours.Debug);
            WriteToConsole($"Is Development: {verb.IsDevelopmentEnvironment}", ConsoleColours.Debug);

            SendImportAccountLevyDeclarationsCommand(endpoint, verb.AccountId, verb.PayeRef);
        }
        private static void SendImportAccountLevyDeclarationsCommand(IMessageSession endpoint, long accountId, string payeRef)
        {
            try
            {
                WriteToConsole($"Sending Message account id: {accountId}, PAYE Scheme: {payeRef}", ConsoleColours.Debug);

                endpoint.Send(new ImportAccountLevyDeclarationsCommand
                {
                    AccountId = accountId,
                     PayeRef = payeRef
                }).GetAwaiter().GetResult();

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