using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;

public static class MessageSessionExtensions
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    
    public static void SendCommand<T>(this IMessageSession endpoint, T command)
    {
        var serializedCommand = JsonSerializer.Serialize(command, Options);
        
        WriteToConsole($"Sending Message '{typeof(T).ToString().Split('.').Last()}' with payload:{Environment.NewLine}{serializedCommand}", ConsoleColours.Debug);

        try
        {
            endpoint.Send(command).GetAwaiter().GetResult();
        }
        catch (Exception exception)
        {
            WriteToConsole("Failed to send command: " + exception, ConsoleColours.Error);
            throw;
        }
        
        WriteToConsole("Message sent successfully", ConsoleColours.Success);
    }
}