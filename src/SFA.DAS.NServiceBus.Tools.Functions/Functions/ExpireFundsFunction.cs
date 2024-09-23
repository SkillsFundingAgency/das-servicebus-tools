using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ExpireFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("ExpireFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<ExpireFundsCommand>();
        }
        catch
        {
            return new BadRequestObjectResult("ExpireFunds failed.");
        }

        return new OkObjectResult("ExpireFunds completed successfully.");
    }
}