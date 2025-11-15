using Lab0.VendingMachine.Core.Enums;

namespace Lab0.VendingMachine.App;

public class AdminPanel(VendingMachine vendingMachine)
{ 
    private Action _onExit;

    public void Show(Action onExit = null)
    {
        _onExit = onExit;
        
        var adminCommands = new Dictionary<string, Command>()
        {
            ["1"] = new ("Режим редактироания",  Editor),
            ["2"] = new ("Внесение средств", AddCoins),
            ["3"] = new ("Сбор средств", Fundraising),
            ["4"] = new ("Просмотр средств", DisplayCoins),
            ["5"] = new ("Выход", () =>  _onExit?.Invoke())
        };
        MessageColor.ShowMessage("== Панель администратора ==", ConsoleColor.Blue);
        vendingMachine.DisplayProducts();
        Console.WriteLine("Доступные команды:");
        foreach (var cmd in adminCommands)
        {
            Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
        }

        string choice = Console.ReadLine().Trim();
        if (adminCommands.ContainsKey(choice))
        {
            adminCommands[choice].Action();
        }
        else
        {
            MessageColor.ShowMessage("Такого выбора нет:(", ConsoleColor.Red);
            Show(onExit);
        }
        
    }

    private void Editor()
    {
        var editingCommands = new Dictionary<string, Command>()
        {
            ["1"] = new ("Добавление товара(изменение количества товара)",  AddProduct),
            ["2"] = new ("Изменить цену на товар", UpdateProductPrice),
            ["3"] = new ("Удалить товар", RemoveProduct),
            ["4"] = new ("Выход в меню администратора", () => Show(_onExit))
        };
        
        MessageColor.ShowMessage("== Режим редактирования ==", ConsoleColor.Blue);
        Console.WriteLine("Доступные команды:");
        foreach (var cmd in editingCommands)
        {
            Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
        }

        string choice = Console.ReadLine().Trim();
        if (editingCommands.ContainsKey(choice))
        {
            editingCommands[choice].Action();
        }
        else
        {
            MessageColor.ShowMessage("Такого выбора нет:(", ConsoleColor.Red);
            Editor();
        }
    }
    
    private void AddProduct()
    {
        var choiceCommands = new Dictionary<string, Command>()
        {
            ["1"] = new ("Вернуться в режим редактирования", () => Editor()),
            ["2"] = new ("Нет", () => AddProduct())
        };
        try
        {
            Console.Write("Введите название продукта: ");
            string productName = Console.ReadLine().Trim();
            ExceptionHandling.CheckName(productName);
            
            if (vendingMachine.HasProduct(productName))
            {
                MessageColor.ShowMessage($"Товар с именем {productName} уже существует! Изменить количество?", 
                    ConsoleColor.Red);
                choiceCommands["3"] = new Command("Да", () => UpdateProductQuantity(productName));

                foreach (var cmd in choiceCommands)
                {
                    Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
                }

                string choice = Console.ReadLine().Trim();
                if (choiceCommands.ContainsKey(choice))
                {
                    choiceCommands[choice].Action();
                }
                else
                {
                    MessageColor.ShowMessage("Такого выбора нет:(", ConsoleColor.Red);
                    Editor();
                }

                AddProduct();
            }

            Console.Write("Введите цену продукта: ");
            string price = Console.ReadLine().Trim();
            ExceptionHandling.CheckPrice(price);
            decimal productPrice = Decimal.Parse(price);
            
            Console.Write("Введите количество продукта: ");
            string quantity = Console.ReadLine().Trim();
            ExceptionHandling.CheckQuantity(quantity);
            int productQuantity = int.Parse(quantity);

            vendingMachine.AddProduct(productName, productPrice, productQuantity);
            
            if (vendingMachine.HasProduct(productName))
                MessageColor.ShowMessage("Товар добавлен", ConsoleColor.DarkGreen);
            Editor();
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
            AddProduct();
        }
        
    }
    
    private void UpdateProductQuantity(string name)
    {
        try
        {
            Console.Write("Введите количество: ");
            string quantity = Console.ReadLine().Trim();
            ExceptionHandling.CheckQuantity(quantity);
            int newQuantity = int.Parse(quantity);
            
            vendingMachine.UpdateProductQuantity(name, newQuantity);
            MessageColor.ShowMessage("Количество обновлено!", ConsoleColor.DarkGreen);
            Editor();
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
        }
    }

    private void UpdateProductPrice()
    {
        try
        {
            if (vendingMachine.DisplayProducts())
            {
                Console.Write("Введите ID товара: ");
                string id = Console.ReadLine().Trim();
                ExceptionHandling.CheckId(id, vendingMachine);
                int productId = int.Parse(id);


                Console.Write("Введите новую цену: ");
                string price = Console.ReadLine().Trim();
                ExceptionHandling.CheckPrice(price);
                decimal newPrice = Decimal.Parse(price);

                vendingMachine.UpdateProductPrice(productId, newPrice);

                MessageColor.ShowMessage("Цена обновлена", ConsoleColor.DarkGreen);
            }
            
            Editor();

        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
            UpdateProductPrice();
        }
    }

    private void RemoveProduct()
    {
        try
        {
            if (vendingMachine.DisplayProducts())
            {
                Console.Write("Введите ID товара, который хотите удалить: ");
                string id = Console.ReadLine().Trim();
                ExceptionHandling.CheckId(id, vendingMachine);
                int productId = int.Parse(id);

                vendingMachine.RemoveProduct(productId);
                MessageColor.ShowMessage("Товар успешно удален", ConsoleColor.DarkGreen);
            }
            Editor();
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
            RemoveProduct();
        }
    }

    private void DisplayCoins()
    {
        vendingMachine.DisplayCoins();
        Show(_onExit);
    }
    
    private void Fundraising()
    {
        try
        {
            Console.WriteLine("Доступные номиналы для извлечения: ");
            foreach (var (denomination, quantity) in vendingMachine.GetAvailableCoinsWithQuantities())
            {
                Console.WriteLine($"{(int)denomination} руб. (доступно: {quantity} шт.)");
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
            vendingMachine.Fundraising(selectedDenomination, selectedQuantity);
            MessageColor.ShowMessage("Средства извлечены", ConsoleColor.DarkGreen);
            Show(_onExit);
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message, ConsoleColor.DarkRed);
            Fundraising();
        }
    }

    private void AddCoins()
    {
        try
        {
            Console.WriteLine("Доступные номиналы: ");
            foreach (var (denomination, quantity) in vendingMachine.GetAvailableCoinsWithQuantities())
            {
                Console.WriteLine($"{(int)denomination} руб. (доступно: {quantity} шт.)");
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
            
            vendingMachine.AddCoin(selectedDenomination, selectedQuantity);
            
            MessageColor.ShowMessage($"Внесено {selectedQuantity} монет по {denominationValue} руб.",
                ConsoleColor.DarkGreen);
            Show(_onExit);
        }
        catch (ArgumentException e)
        {
            MessageColor.ShowMessage(e.Message,  ConsoleColor.DarkRed);
            AddCoins();
        }

    }
}