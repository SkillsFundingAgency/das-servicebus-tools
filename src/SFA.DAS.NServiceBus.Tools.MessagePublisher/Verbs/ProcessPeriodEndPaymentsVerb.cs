using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

[Verb("processperiodendpayments", HelpText = "Import payments of a period end for all accounts.")]
public class ProcessPeriodEndPaymentsVerb : NServiceBusVerbBase
{
    [Option('p', "period", HelpText = "The period end you wish to import payment for.")] 
    public string? PeriodEnd { get; set; }
}