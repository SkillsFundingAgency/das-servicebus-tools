using System;
using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class DraftExpireFundsCommand : ICommand
{
    public DateTime? DateTo { get; set; }
}