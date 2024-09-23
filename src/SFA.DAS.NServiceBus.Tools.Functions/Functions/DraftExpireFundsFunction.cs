using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class DraftExpireFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("DraftExpireFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "DraftExpireFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<DraftExpireFundsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("DraftExpireFunds failed.");
        }

        return new OkObjectResult("DraftExpireFunds completed successfully.");
    }
}