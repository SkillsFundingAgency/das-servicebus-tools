using Microsoft.Extensions.Logging;
using SFA.DAS.ServiceBus.Tools.Functions.Services;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class ExpireFundsFunction(IMessageProcessor messageProcessor)
{
    [Function("ExpireFunds")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ExpireFunds")] HttpRequestData req, FunctionContext functionContext)
    {
        var logger = functionContext.GetLogger(nameof(ExpireFundsFunction));
        logger.LogInformation("ExpireFundsFunction processing started.");
        
        try
        {
            await messageProcessor.SendCommand<ExpireFundsCommand>(functionContext);
        }
        catch(Exception exception)
        {
            logger.LogError(exception, "Exception thrown in ExpireFundsFunction.");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}