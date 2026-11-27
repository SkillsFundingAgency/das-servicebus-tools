using SFA.DAS.Payments.External.Messages.Events.HiringGrant;
using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class RequestingPreEmploymentVerificationFunction(IMessageProcessor messageProcessor)
{
    [Function("RequestingPreEmploymentVerification")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "RequestingPreEmploymentVerification")] HttpRequestData req,
        FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.PublishEvent<RequestingPreEmploymentVerification>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}
