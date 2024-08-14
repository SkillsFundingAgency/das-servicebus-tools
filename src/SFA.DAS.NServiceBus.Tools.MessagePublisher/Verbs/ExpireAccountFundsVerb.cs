using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

[Verb("expireaccountfunds", HelpText = "Expire funds for an account.")]
public class ExpireAccountFundsVerb : NServiceBusVerbBase
{
    [Option('a', "account", HelpText = "The account ID you wish to expire funds for.")] 
    public long AccountId { get; set; }
}