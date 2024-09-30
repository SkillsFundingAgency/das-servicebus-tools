using NServiceBus;
using SFA.DAS.EmployerFinance.Messages.Commands;

namespace SFA.DAS.ServiceBus.Tools.Functions.Extensions;

public static class RoutingExtensions
{
    private const string FinanceMessageHandlersEndpoint = "SFA.DAS.EmployerFinance.MessageHandlers";
    
    public static void AddRouting(this RoutingSettings routing)
    {
        routing.RouteToEndpoint(typeof(DraftExpireAccountFundsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(DraftExpireFundsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(ExpireAccountFundsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(ExpireFundsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(ImportAccountLevyDeclarationsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(ImportPaymentsCommand), FinanceMessageHandlersEndpoint);
        routing.RouteToEndpoint(typeof(ProcessPeriodEndPaymentsCommand), FinanceMessageHandlersEndpoint);
    }
}