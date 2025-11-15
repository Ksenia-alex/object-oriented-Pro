namespace Lab0.VendingMachine.App;

public class MainController
{
    private readonly AuthenticationService _authService = new();
    private readonly VendingMachine _vendingMachine = new();
    private readonly AdminPanel _adminPanel;
    private readonly ClientPanel _clientPanel;

    public MainController()
    {
        _adminPanel = new AdminPanel(_vendingMachine);
        _clientPanel = new ClientPanel(_vendingMachine);
    }

    public void Run()
    {
        while (true)
        {
            var mainCommands = new Dictionary<string, Command>()
            {
                ["1"] = new("Режим администратора", () => CheckPassword()),
                ["2"] = new("Режим клиента", () => _clientPanel.Show (() => Run())),
                ["3"] = new("Выход", () => Environment.Exit(0))
            };
            MessageColor.ShowMessage("ВЕНДИНГОВЫЙ АВТОМАТ", ConsoleColor.DarkBlue);
            MessageColor.ShowMessage("Выберите режим использования:", ConsoleColor.Blue);
            foreach (var cmd in mainCommands)
            {
                Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
            }

            string choice = Console.ReadLine().Trim();

            if (mainCommands.ContainsKey(choice))
            {
                mainCommands[choice].Action();
            }
            else
            {
                MessageColor.ShowMessage("Такого выбора нет:(", ConsoleColor.Red);
            }
        }
    }

    private void CheckPassword()
    {
        for (int i = 0; i < 3; i++) {
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine().Trim();

            if (_authService.ValidatePassword(password))
            {
                _adminPanel.Show(() => Run());
                return;
            }
            MessageColor.ShowMessage("Неверный пароль! Повторите попытку", ConsoleColor.Red);
        }
        MessageColor.ShowMessage(
            "Вы ыыели неверный пароль три раза. Повторите действие или выберите режим клиента",
            ConsoleColor.DarkRed);
    }
}
