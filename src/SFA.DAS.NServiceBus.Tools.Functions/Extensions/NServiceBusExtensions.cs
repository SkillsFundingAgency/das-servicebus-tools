using System.IO;
using System.Net;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace SFA.DAS.NServiceBus.Tools.Functions.Extensions;

public static class NServiceBusExtensions
{
    private const string EndpointName = nameof(SFA.DAS.NServiceBus.Tools.Functions);
    private const string ErrorEndpointName = $"{EndpointName}-error";

    public static IHostBuilder ConfigureNServiceBus(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseNServiceBus((config, endpointConfiguration) =>
        {
            endpointConfiguration.Routing.AddRouting();
            endpointConfiguration.AdvancedConfiguration.EnableInstallers();
            endpointConfiguration.AdvancedConfiguration.SendFailedMessagesTo(ErrorEndpointName);
            
            if (!string.IsNullOrEmpty(config["NServiceBusLicense"]))
            {
                var decodedLicence = WebUtility.HtmlDecode(config["NServiceBusLicense"]);
                endpointConfiguration.AdvancedConfiguration.License(decodedLicence);
            }

#if DEBUG
            var transport = endpointConfiguration.AdvancedConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory(Path.Combine(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("src")),
                @"src\.learningtransport"));
#endif
        });

        return hostBuilder;
    }
}