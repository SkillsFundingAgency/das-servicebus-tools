using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ExpireAccountFundsFunction(IMessageSession messageSession)
{
    [Function("ExpireAccountFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireAccountFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<ExpireAccountFundsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("ExpireAccountFunds failed.");
        }

        return new OkObjectResult("ExpireAccountFunds completed successfully.");
    }
}