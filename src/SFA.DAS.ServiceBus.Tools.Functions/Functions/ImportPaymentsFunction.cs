using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class ImportPaymentsFunction(IMessageProcessor messageProcessor)
{
    [Function("ImportPayments")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportPayments")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.SendCommand<ImportPaymentsCommand>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}