using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using NServiceBus;
using SFA.DAS.ServiceBus.Tools.Functions.Extensions;
using SFA.DAS.ServiceBus.Tools.Functions.Services;

[assembly: NServiceBusTriggerFunction("SFA.DAS.ServiceBus.Tools.Functions")]

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(builder => builder.BuildDasConfiguration())
    .ConfigureNServiceBus()
    .ConfigureServices((context, services) =>
    {
        services.AddLogging(builder =>
        {
            builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
            builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);

            builder.AddConsole();
        });

        var configuration = context.Configuration;

        // Environment.SetEnvironmentVariable("AzureWebJobsServiceBus", configuration["AzureWebJobsStorage"]);
        // Environment.SetEnvironmentVariable("NSERVICEBUS_LICENSE", "TESTING");

        services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), configuration));
        services.AddOptions();
        services.AddSingleton(configuration);

        services.AddTransient<IMessageProcessor, MessageProcessor>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();