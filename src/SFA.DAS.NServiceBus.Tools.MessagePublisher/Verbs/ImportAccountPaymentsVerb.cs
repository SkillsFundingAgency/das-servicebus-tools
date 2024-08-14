using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

[Verb("importpayments", HelpText = "Import payments of a period end into an account.")]
public class ImportAccountPaymentsVerb : NServiceBusVerbBase
{
    [Option('a', "account", HelpText = "The account ID you wish to import payments to.")] 
    public long AccountId { get; set; }
    [Option('p', "period", HelpText = "The period end you wish to import payment for.")] 
    public string? PeriodEnd { get; set; }
}