using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.ServiceBus.Tools.Functions.Functions;

public class TestFunction
{
    [Function("TestFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "Test")] HttpRequestData req, FunctionContext functionContext)
    {
        var logger = functionContext.GetLogger(nameof(ExpireFundsFunction));
        logger.LogInformation("TestFunction processing started.");

        return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK));
    }
}