using Lab0.VendingMachine.Core.Enums;

namespace Lab0.VendingMachine.App;

public static class ExceptionHandling
{
    public static void CheckName(string nameInput)
    {
        if (string.IsNullOrWhiteSpace(nameInput))
            throw new ArgumentException("Ошибка: Название не может быть пустым!");
    }

    public static void CheckPrice(string priceInput)
    {
        if (string.IsNullOrWhiteSpace(priceInput))
            throw new ArgumentException("Ошибка: цена не может быть пустой!");
        
        if (!decimal.TryParse(priceInput, out _))
            throw new ArgumentException("Ошибка: цена должна быть числом!");

        if (Decimal.Parse(priceInput) < 0)
            throw new ArgumentException("Ошибка: ценв должна быть положительным числом!");
    }

    public static void CheckQuantity(string quantityInput)
    {
        if (string.IsNullOrWhiteSpace(quantityInput))
            throw new ArgumentException("Ошибка: количество не может быть пустым!");
        
        if (!decimal.TryParse(quantityInput, out _))
            throw new ArgumentException("Ошибка: количество должно быть числом!");
        
        if (int.Parse(quantityInput) < 0)
            throw new ArgumentException("Ошибка: количество должно быть положительным числом!");
    }

    public static void CheckDenomination(string denominationInput)
    {
        if (string.IsNullOrWhiteSpace(denominationInput))
            throw new ArgumentException("Ошибка: номинал не может быть пустым!");
        
        if (!int.TryParse(denominationInput, out _))
            throw new ArgumentException("Ошибка: номинал должен быть числом!");
        
        if (!Enum.IsDefined(typeof(CoinDenomination), int.Parse(denominationInput)))
            throw new ArgumentException("Ошибка: неверный номинал!");
    }

    public static void CheckId(string idInput, VendingMachine vendingMachine)
    {
        if (string.IsNullOrWhiteSpace(idInput))
            throw new ArgumentException("Ошибка: id не может быть пустым!");
        
        if  (!int.TryParse(idInput, out _))
            throw new ArgumentException("Ошибка: id должно быть числом!");
        
        if (!vendingMachine.HasIdProduct(int.Parse(idInput)))
            throw new ArgumentException("Ошибка: ID не найден");
    }
}