using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SFA.DAS.NServiceBus.Tools.Functions.Messages;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ExpireAccountFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("ExpireAccountFunds")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireAccountFunds")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.SendCommand<ExpireAccountFundsCommand>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}