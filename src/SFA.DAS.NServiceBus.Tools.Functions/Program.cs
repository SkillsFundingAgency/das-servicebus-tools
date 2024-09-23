using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using NServiceBus;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

[assembly: NServiceBusTriggerFunction("SFA.DAS.NServiceBus.Tools.Functions")]

const string endpointName = nameof(SFA.DAS.NServiceBus.Tools.Functions);
const string errorEndpointName = $"{endpointName}-error";

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((hostBuilderContext, builder) => { builder.BuildDasConfiguration(hostBuilderContext.Configuration); })
    .ConfigureServices((context, services) =>
    {
        services.AddLogging(builder =>
        {
            builder.AddApplicationInsights();

            builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
            builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);

            builder.AddConsole();

        });

        var configuration = context.Configuration;

        services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), configuration));
        services.AddOptions();

        //services.AddTransient<IMessageProcessor, MessageProcessor>();
        services.AddTransient<IMessageProcessor, StubMessageProcessor>();

        // MI isn't currently supported by NSB in isolation process so NServiceBusConnectionString will need to be a SharedAccessKey in Azure.
        // When NSB SB triggers work with MI, AzureWebJobsServiceBus needs replacing with AzureWebJobsServiceBus:fullyQualifiedNamespace env variable in azure as per
        // https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Microsoft.Azure.WebJobs.Extensions.ServiceBus/README.md#managed-identity-authentication

        //Environment.SetEnvironmentVariable("AzureWebJobsServiceBus", functionsConfig.NServiceBusConnectionString);
        //Environment.SetEnvironmentVariable("NSERVICEBUS_LICENSE", functionsConfig.NServiceBusLicense);

        Environment.SetEnvironmentVariable("AzureWebJobsServiceBus", configuration["AzureWebJobsStorage"]);
        Environment.SetEnvironmentVariable("NSERVICEBUS_LICENSE", "TESTING");

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    //.UseNServiceBus(endpointName, serviceBusConnectionString, (_, endpointConfiguration) =>
    .UseNServiceBus((_, endpointConfiguration) =>
    {
        endpointConfiguration.Routing.AddRouting();
        endpointConfiguration.AdvancedConfiguration.EnableInstallers();
        endpointConfiguration.AdvancedConfiguration.SendFailedMessagesTo(errorEndpointName);
        endpointConfiguration.AdvancedConfiguration.AssemblyScanner().ExcludeAssemblies("SFA.DAS.NServiceBus");
    })
    .Build();

host.Run();
