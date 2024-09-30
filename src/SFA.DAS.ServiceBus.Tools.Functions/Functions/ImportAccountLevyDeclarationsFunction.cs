using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SFA.DAS.ServiceBus.Tools.Functions.Commands;
using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class ImportAccountLevyDeclarationsFunction(IMessageProcessor messageProcessor)
{
    [Function("ImportAccountLevyDeclarations")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportAccountLevyDeclarations")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.SendCommand<ImportAccountLevyDeclarationsCommand>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}