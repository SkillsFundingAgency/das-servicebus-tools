﻿namespace SFA.DAS.EmployerFinance.Messages.Commands;

public class ImportAccountLevyDeclarationsCommand(long accountId, string payeRef)
{
    public long AccountId { get; set; } = accountId;
    public string PayeRef { get; set; } = payeRef;
}