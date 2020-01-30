using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("draftexpireaccountfunds", HelpText = "Expire funds for an account.")]
    public class DraftExpireAccountFundsVerb : DraftExpireFundsVerb
    {
        [Option('a', "account", HelpText = "The account ID you wish to expire funds for.")] public long AccountId { get; set; }
    }
}