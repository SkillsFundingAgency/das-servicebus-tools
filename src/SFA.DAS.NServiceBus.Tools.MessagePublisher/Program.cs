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

           Parser.Default.ParseArguments(args, typeof(ImportAccountPaymentsVerb))
               .WithParsed<ImportAccountPaymentsVerb>(verb => ImportPaymentsAction.Execute(verb, container));
       }
    }
}
