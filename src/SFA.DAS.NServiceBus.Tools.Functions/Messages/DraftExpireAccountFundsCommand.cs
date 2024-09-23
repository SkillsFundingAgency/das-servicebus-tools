using System;
using NServiceBus;

namespace SFA.DAS.NServiceBus.Tools.Functions.Messages;

public class DraftExpireAccountFundsCommand : ICommand
{
    public long AccountId { get; set; }
    public DateTime? DateTo { get; set; }
}