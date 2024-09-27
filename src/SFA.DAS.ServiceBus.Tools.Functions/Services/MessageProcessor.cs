using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NServiceBus;

namespace SFA.DAS.ServiceBus.Tools.Functions.Services;

public interface IMessageProcessor
{
    Task SendCommand<T>(FunctionContext context) where T : class;
    Task SendCommand<T>(Stream requestContent, FunctionContext context) where T : class;
}

public class StubMessageProcessor(ILogger<StubMessageProcessor> logger) : IMessageProcessor
{
    public Task SendCommand<T>(FunctionContext context) where T : class
    {
        var typeName = typeof(T).ToString().Split('.').Last();
        logger.LogInformation("Sending Message '{TypeName)}'", typeName);
        return Task.CompletedTask;
    }

    public async Task SendCommand<T>(Stream requestContent, FunctionContext context) where T : class
    {
        using var reader = new StreamReader(requestContent);
        var typeName = typeof(T).ToString().Split('.').Last();
        var content = await reader.ReadToEndAsync();
        logger.LogInformation("Sending Message '{TypeName)}' with payload: {Payload}", typeName, content);
    }
}

public class MessageProcessor(IFunctionEndpoint endpoint, ILogger<MessageProcessor> logger) : IMessageProcessor
{
    public async Task SendCommand<T>(Stream requestContent, FunctionContext context) where T : class
    {
        using var reader = new StreamReader(requestContent);
        var content = await reader.ReadToEndAsync();
        var command = JsonConvert.DeserializeObject<T>(content);
        var typeName = typeof(T).ToString().Split('.').Last();

        logger.LogInformation("Sending Message '{TypeName)}' with payload: {Payload}", typeName, content);

        await SendCommand(command, typeName, context);
    }

    public async Task SendCommand<T>(FunctionContext context) where T : class
    {
        var typeName = typeof(T).ToString().Split('.').Last();

        logger.LogInformation("Sending Message '{TypeName)}'.", typeName);

        var command = Activator.CreateInstance<T>();

        await SendCommand(command, typeName, context);
    }
    
    private async Task SendCommand<T>(T command, string typeName, FunctionContext context) where T : class
    {
        try
        {
            await endpoint.Send(command, context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to send command {TypeName}.", typeName);
            throw;
        }

        logger.LogInformation("Message '{TypeName}' sent successfully", typeName);
    }
}