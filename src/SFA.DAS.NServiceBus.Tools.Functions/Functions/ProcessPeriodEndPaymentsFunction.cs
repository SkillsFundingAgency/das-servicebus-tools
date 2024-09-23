using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ProcessPeriodEndPaymentsFunction(IMessageProcessor messageProcessor)
{
    [Function("ProcessPeriodEndPayments")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ProcessPeriodEndPayments")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<ProcessPeriodEndPaymentsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("ProcessPeriodEndPayments failed.");
        }

        return new OkObjectResult("ProcessPeriodEndPayments completed successfully.");
    }
}