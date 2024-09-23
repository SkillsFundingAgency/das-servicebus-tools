using NServiceBus;

namespace SFA.DAS.NServiceBus.Tools.Functions.Messages;

public class ExpireAccountFundsCommand : ICommand
{
    public long AccountId { get; set; }
}