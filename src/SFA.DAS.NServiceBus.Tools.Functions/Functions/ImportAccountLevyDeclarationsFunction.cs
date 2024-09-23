using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Extensions;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ImportAccountLevyDeclarationsFunction(IMessageSession messageSession)
{
    [Function("ImportAccountLevyDeclarations")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportAccountLevyDeclarations")] HttpRequest req, ILogger log)
    {
        try
        {
            await messageSession.SendCommand<ImportAccountLevyDeclarationsCommand>(req.Body, log);
        }
        catch
        {
            return new BadRequestObjectResult("ImportAccountLevyDeclarations failed.");
        }

        return new OkObjectResult("ImportAccountLevyDeclarations completed successfully.");
    }
}