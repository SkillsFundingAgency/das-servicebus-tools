using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("draftexpireaccountfunds", HelpText = "Expire funds for an account.")]
    public class DraftExpireAccountFundsVerb : NServiceBusVerbBase
    {
        [Option('a', "account", HelpText = "The account ID you wish to expire funds for.")] public long AccountId { get; set; }
        [Option('m', "month", HelpText = "The date for which you wish to run expiry up to e.g. 5")] public int Month { get; set; }
        [Option('y', "year", HelpText = "The year for which you wish to run expiry up to e.g. 2018")] public int Year { get; set; }
    }
}