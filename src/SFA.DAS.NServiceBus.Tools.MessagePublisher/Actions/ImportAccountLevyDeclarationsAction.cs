using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;

public static class ImportAccountLevyDeclarationsAction
{
    public static void Execute(ImportAccountLevyDeclarationsVerb verb, IServiceCollection services)
    {
        WriteDebugToConsole(verb);
        
        WriteToConsole("Connecting to NServiceBus endpoint:", ConsoleColours.Debug);

        var endpoint = EndpointExtensions.StartEndpoint<ExpireAccountFundsCommand>(verb, services);
        
        var command = new ImportAccountLevyDeclarationsCommand(verb.AccountId, verb.PayeRef);
        
        endpoint.SendCommand(command);
    }
}