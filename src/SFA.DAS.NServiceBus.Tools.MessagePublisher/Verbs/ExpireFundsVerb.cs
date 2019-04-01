using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("expirefunds", HelpText = "Expire funds for all accounts.")]
    public class ExpireFundsVerb : NServiceBusVerbBase
    {
    }
}