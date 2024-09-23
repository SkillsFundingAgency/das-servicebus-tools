using NServiceBus;
using SFA.DAS.NServiceBus.Tools.Functions.Messages;


namespace SFA.DAS.NServiceBus.Tools.Functions.Extensions;

public static class RoutingExtensions
{
    public static void AddRouting(this RoutingSettings routing)
    {
        routing.RouteToEndpoint(typeof(DraftExpireAccountFundsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
        routing.RouteToEndpoint(typeof(DraftExpireAccountFundsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
        routing.RouteToEndpoint(typeof(ExpireAccountFundsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
        routing.RouteToEndpoint(typeof(ImportAccountLevyDeclarationsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
        routing.RouteToEndpoint(typeof(ImportPaymentsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
        routing.RouteToEndpoint(typeof(ProcessPeriodEndPaymentsCommand), "SFA.DAS.EmployerFinance.MessageHandlers");
    }
}