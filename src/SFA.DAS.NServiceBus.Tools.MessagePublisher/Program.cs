using CommandLine;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Actions;
using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

var services = new ServiceCollection();

Parser.Default.ParseArguments(args,
        typeof(ProcessPeriodEndPaymentsVerb),
        typeof(ImportAccountPaymentsVerb),
        typeof(ImportAccountLevyDeclarationsVerb),
        typeof(ExpireFundsVerb),
        typeof(ExpireAccountFundsVerb),
        typeof(DraftExpireFundsVerb),
        typeof(DraftExpireAccountFundsVerb)
    )
    .WithParsed<ProcessPeriodEndPaymentsVerb>(verb => ProcessPeriodEndPaymentsAction.Execute(verb, services))
    .WithParsed<ImportAccountPaymentsVerb>(verb => ImportPaymentsAction.Execute(verb, services))
    .WithParsed<ImportAccountLevyDeclarationsVerb>(verb => ImportAccountLevyDeclarationsAction.Execute(verb, services))
    .WithParsed<ExpireFundsVerb>(verb => ExpireFundsAction.Execute(verb, services))
    .WithParsed<ExpireAccountFundsVerb>(verb => ExpireAccountFundsAction.Execute(verb, services))
    .WithParsed<DraftExpireFundsVerb>(verb => DraftExpireFundsAction.Execute(verb, services))
    .WithParsed<DraftExpireAccountFundsVerb>(verb => DraftExpireAccountFundsAction.Execute(verb, services));