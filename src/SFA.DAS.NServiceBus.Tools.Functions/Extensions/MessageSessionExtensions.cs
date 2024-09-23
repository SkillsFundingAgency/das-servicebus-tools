using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SFA.DAS.NServiceBus.Tools.Functions.Extensions;

public static class MessageSessionExtensions
{
    public static async Task SendCommand<T>(this IMessageSession endpoint, Stream requestContent, ILogger logger) where T : class, ICommand
    {
        var command = JsonConvert.DeserializeObject<T>(requestContent.ToString());
        var typeName = typeof(T).ToString().Split('.').Last();
        
        logger.LogInformation("Sending Message '{Type)}' with payload: {Payload}", typeName, requestContent);

        await endpoint.SendCommand(logger, command, typeName);
    }
    
    public static async Task SendCommand<T>(this IMessageSession endpoint, ILogger logger) where T : class, ICommand
    {
       var typeName = typeof(T).ToString().Split('.').Last();
        
        logger.LogInformation("Sending Message '{Type)}'.", typeName);

        var command = Activator.CreateInstance<T>();

        await endpoint.SendCommand(logger, command, typeName);
    }

    private static async Task SendCommand<T>(this IMessageSession endpoint, ILogger logger, T command, string typeName) where T : class, ICommand
    {
        try
        {
            await endpoint.Send(command);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to send command {Type}.", typeName);
            throw;
        }

        logger.LogInformation("Message '{TypeName}' sent successfully", typeName);
    }
}