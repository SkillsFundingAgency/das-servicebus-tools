using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ImportAccountLevyDeclarationsFunction(IMessageProcessor messageProcessor)
{
    [Function("ImportAccountLevyDeclarations")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportAccountLevyDeclarations")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageProcessor.SendCommand<ImportAccountLevyDeclarationsCommand>(req.Body);
        }
        catch
        {
            return new BadRequestObjectResult("ImportAccountLevyDeclarations failed.");
        }

        return new OkObjectResult("ImportAccountLevyDeclarations completed successfully.");
    }
}