using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ProcessPeriodEndPaymentsFunction(IMessageSession messageSession)
{
    [Function("ProcessPeriodEndPayments")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ProcessPeriodEndPayments")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<ProcessPeriodEndPaymentsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("ProcessPeriodEndPayments failed.");
        }

        return new OkObjectResult("ProcessPeriodEndPayments completed successfully.");
    }
}