namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class ProcessPeriodEndPaymentsCommand
{
    public string PeriodEndRef { get; init; }
    public int BatchNumber { get; init; } = 0;
}