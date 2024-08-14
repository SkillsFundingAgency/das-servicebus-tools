using System.Net;
using NServiceBus.ObjectBuilder.MSDependencyInjection;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.MicrosoftDependencyInjection;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;

public static class EndpointExtensions
{
    public static IEndpointInstance StartEndpoint<T>(NServiceBusVerbBase verb, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(verb.ServiceBusConnectionString);
        ArgumentNullException.ThrowIfNull(verb.EndpointName);
        
        var endpointConfiguration = new EndpointConfiguration(verb.EndpointName)
            .UseAzureServiceBusTransport<T>(verb.ServiceBusConnectionString, verb.IsDevelopmentEnvironment, verb.EndpointName)
            .UseLicense(WebUtility.HtmlDecode(verb.License))
            .UseNewtonsoftJsonSerializer()
            .UseSendOnly()
            .UseServicesBuilder(new UpdateableServiceProvider(services));

        return Endpoint
            .Start(endpointConfiguration)
            .GetAwaiter()
            .GetResult();
    }

    private static EndpointConfiguration UseAzureServiceBusTransport<T>(this EndpointConfiguration config, string connectionString, bool isDevelopment, string destination)
    {
        config.UseAzureServiceBusTransport(connectionString, settings =>
        {
            settings.RouteToEndpoint(
                typeof(T).Assembly,
                typeof(T).Namespace,
                destination);
        });

        return config;
    }
}