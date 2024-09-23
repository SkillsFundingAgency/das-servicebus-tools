using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ExpireFundsFunction(IMessageSession messageSession)
{
    [Function("ExpireFundsFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireFundsFunction")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<ExpireFundsCommand>(log);
        }
        catch
        {
            return new BadRequestObjectResult("ExpireFundsFunction failed.");
        }

        return new OkObjectResult("ExpireFundsFunction completed successfully.");
    }
}