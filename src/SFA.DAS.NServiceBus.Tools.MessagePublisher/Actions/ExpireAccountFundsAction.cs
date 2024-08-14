using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;

public static class ExpireAccountFundsAction
{
    public static void Execute(ExpireAccountFundsVerb verb, IServiceCollection services)
    {
        WriteDebugToConsole(verb);
        
        WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);

        var endpoint = EndpointExtensions.StartEndpoint<ExpireAccountFundsCommand>(verb, services);

        var command = new ExpireAccountFundsCommand
        {
            AccountId = verb.AccountId
        };

        endpoint.SendCommand(command);
    }
}