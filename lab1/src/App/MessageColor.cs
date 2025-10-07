namespace object_oriented_Pro.lab1.App;

public static class MessageColor
{
    public static void ShowMessage(string msg, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        Console.WriteLine(msg);

        Console.ResetColor();
    }
}