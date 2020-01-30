using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("draftexpirefunds", HelpText = "Create draft expiry's for all accounts.")]
    public class DraftExpireFundsVerb : NServiceBusVerbBase
    {
    }
}