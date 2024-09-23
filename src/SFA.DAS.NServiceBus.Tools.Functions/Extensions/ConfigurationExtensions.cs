using Microsoft.Extensions.Configuration;

namespace SFA.DAS.NServiceBus.Tools.Functions.Extensions;

public static class ConfigurationExtensions
{
    public static IConfiguration BuildDasConfiguration(this IConfigurationBuilder configBuilder, IConfiguration configuration)
    {
        configBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables();

#if DEBUG
        configBuilder.AddJsonFile("local.settings.json", optional: true);
#endif
        // configBuilder.AddAzureTableStorage(options =>
        // {
        //     options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
        //     options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
        //     options.EnvironmentName = configuration["EnvironmentName"];
        //     options.PreFixConfigurationKeys = false;
        // });

        return configBuilder.Build();
    }
}