using object_oriented_Pro.lab0.Core.Enums;
using object_oriented_Pro.lab0.Core.Models;

namespace object_oriented_Pro.lab0.App;

public class VendingMachine
{
    private readonly List<Product> _products = new();
    private readonly Dictionary<CoinDenomination, Money> _coins = new();

    public VendingMachine()
    {
        InitializeCoins();
    }

    private void InitializeCoins()
    {
        foreach (CoinDenomination denomination in Enum.GetValues(typeof(CoinDenomination)))
        {
            _coins[denomination] = new Money(denomination, 50);
        }
    }
    
    public bool DisplayProducts()
    {
        MessageColor.ShowMessage("=== АССОРТИМЕНТ ТОВАРОВ ===", ConsoleColor.DarkCyan);
        if (_products.Count == 0)
        {
            Console.WriteLine("Товары отсутствуют");
            return false;
        }

        foreach (var product in _products)
        {
            Console.WriteLine($"{product.Id}. {product.Name} - Стоимость: {product.Price}  Количество: {product.Quantity}");
        }
        return true;
    }

    
    
    public void AddProduct(string name, decimal price,  int quantity)
    {
       int id = _products.Count + 1;
            
       _products.Add(new Product(id, name, price, quantity));
    }

    public void UpdateProductQuantity(string name, int quantity)
    {
        var product = _products.First(p => p.Name == name);
        product.Quantity = quantity;
    }

    public void UpdateProductPrice(int id, decimal price)
    {
        var product = _products.First(p => p.Id == id);
        product.Price = price;
    }

    public void RemoveProduct(int id)
    {
        _products.Remove(_products.First(p => p.Id == id));
        
        for (int i = 0; i < _products.Count; i++)
        {
            _products[i].Id = i + 1;
        }
    }

    public void SelectProduct(int id)
    {
        var product = _products.First(p => p.Id == id);

        if (product.Quantity <= 0)
            throw new AggregateException("Товар закончился");
        product.Quantity--;
        if (product.Quantity == 0)
        {
            _products.Remove(product);
            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].Id = i + 1;
            }
        }
    }
    public Product GetProduct(int id)
    {
        return _products.First(p => p.Id == id);
    }
    
    
    
    public bool HasProduct(string productName)
    {
        return _products.Any(p => p.Name == productName);
    }
    
    public bool HasIdProduct(int productid)
    {
        return _products.Any(p => p.Id == productid);
    }

    public bool HasAnyProducts()
    {
        return _products.Any();
    }

    

    public void AddCoin(CoinDenomination denomination, int quantity)
    {
        if (_coins.ContainsKey(denomination))
        {
            _coins[denomination].Quantity += quantity;
        }
        else
        {
            _coins[denomination] = new Money(denomination, quantity);
        }
    }

    public decimal Fundraising(CoinDenomination denomination, int quantity)
    {
        if (!_coins.ContainsKey(denomination) || _coins[denomination].Quantity < quantity)
            throw new AggregateException($"Недостаточно монет номиналом {(int)denomination} руб.");
        _coins[denomination].Quantity -= quantity;
        return (decimal)denomination * quantity;
    }

    public void DisplayCoins()
    {
        MessageColor.ShowMessage("=== СРЕДСТВА В АВТОМАТЕ ===", ConsoleColor.DarkBlue);
        
        decimal total = 0;
        foreach (var coin in _coins.Values.OrderByDescending(c => (int)c.Denomination))
        {
            decimal value = (decimal)coin.Denomination * coin.Quantity;
            total += value;
            Console.WriteLine($"{(int)coin.Denomination} руб.: {coin.Quantity} шт. = {value} руб.");
        }
        
        Console.WriteLine($"ОБЩАЯ СУММА: {total} руб.");
    }

    public List<(CoinDenomination Denomination, int Quantity)> GetAvailableCoinsWithQuantities()
    {
        return _coins.Where(c => c.Value.Quantity > 0)
            .Select(c => (c.Key, c.Value.Quantity))
            .OrderBy(x => (int)x.Key)
            .ToList();
    }

    public Dictionary<CoinDenomination, int> CalculateCoins(decimal remaining)
    {
        if (remaining <= 0)
            return new Dictionary<CoinDenomination, int>();
                
        var returnedCoins = new Dictionary<CoinDenomination, int>();
        
        foreach (var denomination in Enum.GetValues(typeof(CoinDenomination))
                     .Cast<CoinDenomination>()
                     .OrderByDescending(d => (int)d))
        {
            if (remaining <= 0) break;
            
            if (!_coins.ContainsKey(denomination)) continue;

            int coinValue = (int)denomination;
            int numberCoinsInVendingMachine = _coins[denomination].Quantity;
            int needCoins = (int)(remaining / coinValue);
            int coinsCanGiven = Math.Min(numberCoinsInVendingMachine, needCoins);

            if (coinsCanGiven > 0)
            {
                returnedCoins[denomination] = coinsCanGiven;
                _coins[denomination].Quantity -= coinsCanGiven;
                remaining -= coinsCanGiven * coinValue;
            }
        }

        return returnedCoins;
    }
}