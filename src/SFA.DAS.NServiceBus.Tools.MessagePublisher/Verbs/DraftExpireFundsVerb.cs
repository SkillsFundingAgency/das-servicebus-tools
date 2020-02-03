using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("draftexpirefunds", HelpText = "Create draft expiry's for all accounts.")]
    public class DraftExpireFundsVerb : NServiceBusVerbBase
    {
        [Option('m', "month", HelpText = "The date for which you wish to run expiry up to e.g. 5")] public int Month { get; set; }
        [Option('y', "year", HelpText = "The year for which you wish to run expiry up to e.g. 2018")] public int Year { get; set; }
    }
}