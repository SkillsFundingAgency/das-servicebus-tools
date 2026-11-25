using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class LearnerDataFunction(IMessageProcessor messageProcessor)
{
    [Function("LearnerData")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "LearnerData")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.PublishEvent<LearnerDataEvent>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}
