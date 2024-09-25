using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Messages;

public class ImportAccountLevyDeclarationsCommand(long accountId, string payeRef) : ICommand
{
    public long AccountId { get; set; } = accountId;
    public string PayeRef { get; set; } = payeRef;
}