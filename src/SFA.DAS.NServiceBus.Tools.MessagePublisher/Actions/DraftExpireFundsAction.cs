using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;

public static class DraftExpireFundsAction
{
    public static void Execute(DraftExpireFundsVerb verb, IServiceCollection services)
    {
        WriteDebugToConsole(verb);
        
        WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);

        var endpoint = EndpointExtensions.StartEndpoint<ExpireAccountFundsCommand>(verb, services);
        
        var command = new DraftExpireFundsCommand
        {
            DateTo = new DateTime(verb.Year, verb.Month, 1)
        };
        
        endpoint.SendCommand(command);
    }
}