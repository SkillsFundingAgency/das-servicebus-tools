using System.Collections.Generic;
using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Extensions;

public static class RoutingExtensions
{
    private const string FinanceMessageHandlersEndpoint = "SFA.DAS.EmployerFinance.MessageHandlers";

    public static void AddRouting(this RoutingSettings routing)
    {
        AddRoutes([
            typeof(DraftExpireAccountFundsCommand),
            typeof(DraftExpireFundsCommand),
            typeof(ExpireAccountFundsCommand),
            typeof(ExpireFundsCommand),
            typeof(ImportAccountLevyDeclarationsCommand),
            typeof(ImportPaymentsCommand),
            typeof(ProcessPeriodEndPaymentsCommand)
        ], routing);
    }

    private static void AddRoutes(List<Type> types, RoutingSettings routing)
    {
        foreach (var type in types)
        {
            routing.RouteToEndpoint(type, FinanceMessageHandlersEndpoint);
        }
    }
}