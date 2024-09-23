using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SFA.DAS.NServiceBus.Tools.Functions.Services;

public interface IMessageProcessor
{
    Task SendCommand<T>() where T : class, ICommand;
    Task SendCommand<T>(Stream requestContent) where T : class, ICommand;
}

public class StubMessageProcessor(ILogger<StubMessageProcessor> logger) : IMessageProcessor
{
    public Task SendCommand<T>() where T : class, ICommand
    {
        var typeName = typeof(T).ToString().Split('.').Last();
        logger.LogInformation("Sending Message '{TypeName)}'", typeName);
        return Task.CompletedTask;
    }

    public async Task SendCommand<T>(Stream requestContent) where T : class, ICommand
    {
        using var reader = new StreamReader(requestContent);
        var typeName = typeof(T).ToString().Split('.').Last();
        var content = await reader.ReadToEndAsync();
        logger.LogInformation("Sending Message '{TypeName)}' with payload: {Payload}", typeName, content);
    }
}

public class MessageProcessor(IMessageSession session, ILogger<MessageProcessor> logger) : IMessageProcessor
{
    public async Task SendCommand<T>(Stream requestContent) where T : class, ICommand
    {
        using var reader = new StreamReader(requestContent);
        var content = await reader.ReadToEndAsync();
        var command = JsonConvert.DeserializeObject<T>(content);
        var typeName = typeof(T).ToString().Split('.').Last();

        logger.LogInformation("Sending Message '{TypeName)}' with payload: {Payload}", typeName, content);

        await SendCommand(command, typeName);
    }

    public async Task SendCommand<T>() where T : class, ICommand
    {
        var typeName = typeof(T).ToString().Split('.').Last();

        logger.LogInformation("Sending Message '{TypeName)}'.", typeName);

        var command = Activator.CreateInstance<T>();

        await SendCommand(command, typeName);
    }
    
    private async Task SendCommand<T>(T command, string typeName) where T : class, ICommand
    {
        try
        {
            await session.Send(command);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to send command {TypeName}.", typeName);
            throw;
        }

        logger.LogInformation("Message '{TypeName}' sent successfully", typeName);
    }
}