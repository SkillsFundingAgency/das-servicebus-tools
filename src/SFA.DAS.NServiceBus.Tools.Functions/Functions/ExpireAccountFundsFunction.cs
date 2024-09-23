using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ExpireAccountFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("ExpireAccountFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireAccountFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<ExpireAccountFundsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("ExpireAccountFunds failed.");
        }

        return new OkObjectResult("ExpireAccountFunds completed successfully.");
    }
}