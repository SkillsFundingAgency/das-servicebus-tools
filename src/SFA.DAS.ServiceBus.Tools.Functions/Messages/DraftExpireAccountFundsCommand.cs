using System;
using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class DraftExpireAccountFundsCommand : ICommand
{
    public long AccountId { get; set; }
    public DateTime? DateTo { get; set; }
}