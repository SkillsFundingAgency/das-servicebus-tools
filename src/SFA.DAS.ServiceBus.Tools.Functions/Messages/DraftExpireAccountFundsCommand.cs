using System;

namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class DraftExpireAccountFundsCommand 
{
    public long AccountId { get; set; }
    public DateTime? DateTo { get; set; }
}