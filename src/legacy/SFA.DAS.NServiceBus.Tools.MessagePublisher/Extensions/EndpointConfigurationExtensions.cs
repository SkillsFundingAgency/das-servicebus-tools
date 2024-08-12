using System;
using NServiceBus;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions
{
    public static class EndpointConfigurationExtensions
    {
        public static EndpointConfiguration UseAzureServiceBusTransport<T>(this EndpointConfiguration config, string connectionString, bool isDevelopment, string destination)
        {
            config.UseAzureServiceBusTransport(connectionString, r =>
            {
                r.RouteToEndpoint(
                    typeof(T).Assembly,
                    typeof(T).Namespace,
                    destination);
            });

            return config;
        }
    }
}