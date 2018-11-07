using System;
using NServiceBus;
using SFA.DAS.NServiceBus.AzureServiceBus;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher
{
    public static class EndpointConfigurationExtensions
    {
        public static EndpointConfiguration UseAzureServiceBusTransport<T>(this EndpointConfiguration config, Func<string> connectionStringBuilder, bool isDevelopment, string destination)
        {
            config.UseAzureServiceBusTransport(isDevelopment, connectionStringBuilder, r =>
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