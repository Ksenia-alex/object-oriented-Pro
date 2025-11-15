using Lab0.VendingMachine.App;

namespace Lab0.VendingMachine;

public class Program
{
    public static void Main(string[] args)
    {
        var controller = new MainController();
        controller.Run();
    }
}