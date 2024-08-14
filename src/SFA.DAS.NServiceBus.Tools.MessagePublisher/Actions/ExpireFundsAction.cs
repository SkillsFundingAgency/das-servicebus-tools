using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;

public static class ExpireFundsAction
{
    public static void Execute(ExpireFundsVerb verb, IServiceCollection services)
    {
        WriteDebugToConsole(verb);
        
        WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);
        
        var endpoint = EndpointExtensions.StartEndpoint<ExpireAccountFundsCommand>(verb, services);

        endpoint.SendCommand(new ExpireFundsCommand());
    }
}