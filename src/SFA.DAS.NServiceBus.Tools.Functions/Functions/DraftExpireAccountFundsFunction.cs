using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class DraftExpireAccountFundsFunction(IMessageSession messageSession)
{
    [Function("DraftExpireAccountFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "DraftExpireAccountFundsCommand")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<DraftExpireAccountFundsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("DraftExpireAccountFundsCommand failed.");
        }

        return new OkObjectResult("DraftExpireAccountFundsCommand completed successfully.");
    }
}