using NServiceBus;

namespace SFA.DAS.NServiceBus.Tools.Functions.Messages;

public class ProcessPeriodEndPaymentsCommand : ICommand
{
    public string PeriodEndRef { get; init; }
    public int BatchNumber { get; init; } = 0;
}