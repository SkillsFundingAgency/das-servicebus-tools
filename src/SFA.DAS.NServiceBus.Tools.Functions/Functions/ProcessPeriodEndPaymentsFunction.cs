using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.Tools.Functions.Services;

namespace SFA.DAS.NServiceBus.Tools.Functions.Functions;

public class ProcessPeriodEndPaymentsFunction(IMessageProcessor messageProcessor)
{
    [Function("ProcessPeriodEndPayments")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", "ProcessPeriodEndPayments")] HttpRequestData req, FunctionContext functionContext)
    {
        try
        {
            await messageProcessor.SendCommand<ProcessPeriodEndPaymentsCommand>(req.Body, functionContext);
        }
        catch
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}