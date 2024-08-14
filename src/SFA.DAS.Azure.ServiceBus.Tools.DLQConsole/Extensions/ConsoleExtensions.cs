namespace SFA.DAS.Azure.ServiceBus.Tools.DLQConsole.Extensions;

public static class ConsoleExtensions
{
    public static string GetInput(string message)
    {
        string? input;

        do
        {
            Console.WriteLine(message);
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be null");
            }
            
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static void WriteError(string value)
    {
        var previousColour = Console.ForegroundColor;
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(value);
        
        Console.ForegroundColor = previousColour;
    }
}