using SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Extensions;

public static class ConsoleExtensions
{
   public static void WriteToConsole(string text, ConsoleColor color)
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = oldColor;
    }
   
    public static void WriteDebugToConsole(NServiceBusVerbBase verb)
    {
        WriteToConsole("Debug information below:", ConsoleColours.Debug);
        WriteToConsole($"Environment: {verb.Environment}", ConsoleColours.Debug);
        WriteToConsole($"Endpoint: {verb.EndpointName}", ConsoleColours.Debug);
        WriteToConsole($"Service Bus Connection: {verb.ServiceBusConnectionString}", ConsoleColours.Debug);
        WriteToConsole($"License: {verb.License}", ConsoleColours.Debug);
        WriteToConsole($"Is Development: {verb.IsDevelopmentEnvironment}", ConsoleColours.Debug);
    }
}