using Lab0.VendingMachine.Core.Enums;

namespace Lab0.VendingMachine.App;

public class ClientPanel(VendingMachine vendingMachine)
{
    private Action _onExit;
    private decimal _clientBalance = 0;
    
    public void Show(Action onExit = null)
    {
        _onExit = onExit;
        
        var clientCommands = new Dictionary<string, Command>()
        {
            ["1"] = new ("Вставить деньги",  () => InsertCoins()),
            ["2"] = new ("Выбрать товар", () => SelectProduct()),
            ["3"] = new ("Вернуть деньги", () => ReturnMoney()),
            ["4"] = new ("Выход", () =>  _onExit?.Invoke())
        };
        MessageColor.ShowMessage("== Панель клиента ==", ConsoleColor.Blue);
        Console.WriteLine();
        MessageColor.ShowMessage("=== ВНЕСЕННЫЕ СРЕДСТВА ===", ConsoleColor.DarkBlue);
        Console.WriteLine($"Баланс: {_clientBalance} руб.");
        Console.WriteLine();
        vendingMachine.DisplayProducts();
        Console.WriteLine();
        Console.WriteLine("Доступные команды:");
        foreach (var cmd in clientCommands)
        {
            Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
        }

        string choice = Console.ReadLine().Trim();
        if (clientCommands.ContainsKey(choice))
        {
            clientCommands[choice].Action();
        }
        else
        {
            MessageColor.ShowMessage("Такого выбора нет:(", ConsoleColor.Red);
            Show(onExit);
        }
    }

    private void InsertCoins()
    {
        try
        {
            Console.WriteLine("Доступные номиналы: ");
            foreach (CoinDenomination denomination in Enum.GetValues(typeof(CoinDenomination)))
            {
                Console.WriteLine($"{(int)denomination} руб.");
            }

            Console.Write("Выберите номинал: ");
            string denominationInput = Console.ReadLine().Trim();
            ExceptionHandling.CheckDenomination(denominationInput);
            int denominationValue = int.Parse(denominationInput);

            Console.Write("Введите количество: ");
            string quantityInput = Console.ReadLine().Trim();
            ExceptionHandling.CheckQuantity(quantityInput);
            int selectedQuantity = int.Parse(quantityInput);

            var selectedDenomination = (CoinDenomination)denominationValue;
            
            _clientBalance += (int)selectedDenomination * selectedQuantity;
            
            MessageColor.ShowMessage($"Внесено {selectedQuantity} монет по {denominationValue} руб.", 
                ConsoleColor.DarkGreen);
            Show(_onExit);
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
            InsertCoins();
        }
    }

    private void SelectProduct()
    {
        try
        {
            vendingMachine.DisplayProducts();

            if (!vendingMachine.HasAnyProducts())
            {
                MessageColor.ShowMessage("В автомате нет товаров!", ConsoleColor.Red);
                Show(_onExit);
            }

            Console.Write("Введите ID товара для покупки: ");
            string id = Console.ReadLine().Trim();
            ExceptionHandling.CheckId(id, vendingMachine);
            int productId = int.Parse(id);

            var product = vendingMachine.GetProduct(productId);

            if (_clientBalance < product.Price)
            {
                MessageColor.ShowMessage(
                    $"Недостаточно средств! Цена: {product.Price} руб., ваш баланс: {_clientBalance} руб.",
                    ConsoleColor.Red);
                Show(_onExit);
            }

            vendingMachine.SelectProduct(productId);
            
            _clientBalance -= product.Price;

            if (_clientBalance > 0)
            {
                vendingMachine.CalculateCoins(_clientBalance);
                MessageColor.ShowMessage($"Вы купили: {product.Name} за {product.Price} руб.", 
                    ConsoleColor.DarkGreen);
                MessageColor.ShowMessage($"Остаток на балансе: {_clientBalance} руб.", ConsoleColor.DarkBlue);
            }
            Show(_onExit);
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message,  ConsoleColor.DarkRed);
            SelectProduct();
        }
    }
    
    private void ReturnMoney()
    {
        if (_clientBalance <= 0)
        {
            MessageColor.ShowMessage("Нет денег для возврата!", ConsoleColor.Red);
            Show(_onExit);
        }

        try
        {
            var returnCoins = vendingMachine.CalculateCoins(_clientBalance);
            _clientBalance = 0;

            Console.WriteLine("Ваши деньги: ");
            foreach (var (denomination, quantity) in returnCoins)
            {
                MessageColor.ShowMessage($"{quantity} монет по {(int)denomination} руб.", ConsoleColor.DarkGreen);
            }

            decimal total = returnCoins.Sum(c => (int)c.Key * c.Value);
            MessageColor.ShowMessage($"Всего: {total} руб.", ConsoleColor.DarkGreen);
            Show(_onExit);
        }
        catch (AggregateException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
        }
    }
}