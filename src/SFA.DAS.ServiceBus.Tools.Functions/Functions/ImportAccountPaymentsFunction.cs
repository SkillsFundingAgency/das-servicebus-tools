using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class ImportAccountPaymentsFunction(IMessageProcessor messageProcessor)
{
    [Function("ImportAccountPayments")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ImportAccountPayments")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.SendCommand<ImportAccountPaymentsCommand>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}