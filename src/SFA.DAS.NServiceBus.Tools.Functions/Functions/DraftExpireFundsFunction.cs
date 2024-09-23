using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class DraftExpireFundsFunction(IMessageSession messageSession)
{
    [Function("DraftExpireFunds")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "DraftExpireFunds")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<DraftExpireFundsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("DraftExpireFunds failed.");
        }

        return new OkObjectResult("DraftExpireFunds completed successfully.");
    }
}