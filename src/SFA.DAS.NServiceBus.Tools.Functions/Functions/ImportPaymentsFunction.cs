using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ImportPaymentsFunction(IMessageSession messageSession)
{
    [Function("ImportPayments")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportPayments")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<ImportPaymentsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("ImportPayments failed.");
        }

        return new OkObjectResult("ImportPayments completed successfully.");
    }
}