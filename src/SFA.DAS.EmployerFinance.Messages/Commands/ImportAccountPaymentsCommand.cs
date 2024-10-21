namespace SFA.DAS.EmployerFinance.Messages.Commands;

public class ImportAccountPaymentsCommand(long accountId, string periodEndRef)
{
    public long AccountId { get; set; } = accountId;
    public string PeriodEndRef { get; set; } = periodEndRef;
}
