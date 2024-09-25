using System.IO;
using Microsoft.Extensions.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;

namespace SFA.DAS.ServiceBus.Tools.Functions.Extensions;

public static class ConfigurationExtensions
{
    public static IConfiguration BuildDasConfiguration(this IConfigurationBuilder configBuilder)
    {
        configBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        
        var config = configBuilder.Build();

        configBuilder.AddAzureTableStorage(options =>
        {
#if DEBUG
            options.ConfigurationKeys = config["Values:ConfigNames"].Split(",");
            options.StorageConnectionString = config["Values:ConfigurationStorageConnectionString"];
            options.EnvironmentName = config["Values:EnvironmentName"];
#else
            options.ConfigurationKeys = config["ConfigNames"].Split(",");
            options.StorageConnectionString = config["ConfigurationStorageConnectionString"];
            options.EnvironmentName = config["EnvironmentName"];
#endif

            options.PreFixConfigurationKeys = false;
        });
        return configBuilder.Build();
    }
}