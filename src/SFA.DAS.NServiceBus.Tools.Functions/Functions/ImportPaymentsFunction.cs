using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ImportPaymentsFunction(IMessageProcessor messageProcessor)
{
    [Function("ImportPayments")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportPayments")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<ImportPaymentsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("ImportPayments failed.");
        }

        return new OkObjectResult("ImportPayments completed successfully.");
    }
}