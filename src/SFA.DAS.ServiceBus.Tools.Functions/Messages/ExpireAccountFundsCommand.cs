using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class ExpireAccountFundsCommand : ICommand
{
    public long AccountId { get; set; }
}