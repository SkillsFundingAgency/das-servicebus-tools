using System;
using NServiceBus;

namespace SFA.DAS.NServiceBus.Tools.Functions.Messages;

public class DraftExpireFundsCommand : ICommand
{
    public DateTime? DateTo { get; set; }
}