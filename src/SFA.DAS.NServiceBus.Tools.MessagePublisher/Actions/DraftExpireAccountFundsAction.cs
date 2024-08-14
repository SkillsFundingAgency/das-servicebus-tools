using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;

public static class DraftExpireAccountFundsAction
{
    public static void Execute(DraftExpireAccountFundsVerb verb, IServiceCollection services)
    {
        WriteDebugToConsole(verb);
        
        WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);
        
        var endpoint = EndpointExtensions.StartEndpoint<ExpireAccountFundsCommand>(verb, services);
        
        var command = new DraftExpireAccountFundsCommand
        {
            AccountId = verb.AccountId,
            DateTo = new DateTime(verb.Year, verb.Month, 28)
        };
        
        endpoint.SendCommand(command);
    }
}