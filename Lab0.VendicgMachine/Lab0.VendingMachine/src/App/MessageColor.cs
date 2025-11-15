namespace Lab0.VendingMachine.App;

public static class MessageColor
{
    public static void ShowMessage(string msg, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        Console.WriteLine(msg);

        Console.ResetColor();
    }
}