using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

public class NServiceBusVerbBase
{
    [Option('e', "environment", HelpText = "The Environment that the NServiceBus is located in (i.e. LOCAL, TEST, PREPROD).")] 
    public string? Environment { get; set; }
    [Option('n', "endpoint", HelpText = "The NServiceBus endpoint the message will be published to.")] 
    public string? EndpointName { get; set; }
    [Option('c', "connection", HelpText = "The connection string for the target NServiceBus.")] 
    public string? ServiceBusConnectionString { get; set; }
    [Option('l', "license", HelpText = "The license string that contains the NServiceBus license.")] 
    public string? License { get; set; }

    public bool IsDevelopmentEnvironment => !string.IsNullOrEmpty(Environment) &&
                                            Environment.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase);
}