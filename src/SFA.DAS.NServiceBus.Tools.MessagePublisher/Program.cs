using CommandLine;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.DependencyResolution;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher
{
    public class Program
    {
       public static void Main(string[] args)
       {
           var container = IoC.Initialize();

            Parser.Default.ParseArguments(args, typeof(ImportAccountPaymentsVerb), typeof(ImportAccountLevyDeclarationsVerb), typeof(ExpireFundsVerb), typeof(ExpireAccountFundsVerb), typeof(DraftExpireFundsVerb), typeof(DraftExpireAccountFundsVerb))
               .WithParsed<ImportAccountPaymentsVerb>(verb => ImportPaymentsAction.Execute(verb, container))
               .WithParsed<ImportAccountLevyDeclarationsVerb>(verb => ImportAccountLevyDeclarationsAction.Execute(verb, container))
               .WithParsed<ExpireFundsVerb>(verb => ExpireFundsAction.Execute(verb, container))
               .WithParsed<ExpireAccountFundsVerb>(verb => ExpireAccountFundsAction.Execute(verb, container))
               .WithParsed<DraftExpireFundsVerb>(verb => DraftExpireFundsAction.Execute(verb, container))
               .WithParsed<DraftExpireAccountFundsVerb>(verb => DraftExpireAccountFundsAction.Execute(verb, container));
       }
    }
}
