using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class DraftExpireAccountFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("DraftExpireAccountFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "DraftExpireAccountFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<DraftExpireAccountFundsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("DraftExpireAccountFunds failed.");
        }

        return new OkObjectResult("DraftExpireAccountFunds completed successfully.");
    }
}