using System;

namespace SFA.DAS.ServiceBus.Tools.Functions.Commands;

public class DraftExpireAccountFundsCommand 
{
    public long AccountId { get; set; }
    public DateTime? DateTo { get; set; }
}