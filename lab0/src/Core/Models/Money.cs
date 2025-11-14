namespace object_oriented_Pro.lab0.Core.Models;

using object_oriented_Pro.lab0.Core.Enums;

public class Money
{
    public CoinDenomination Denomination
    {
        get => _denomination; 
        set => _denomination = !Enum.IsDefined(typeof(CoinDenomination), value) 
            ? throw new ArgumentException("Invalid coin denomination") 
            : value;
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity  = value >= 0
            ? value
            : throw new AggregateException("Quantity cannot be negative");
    }
    
    private CoinDenomination _denomination;
    private int _quantity;

    public Money(CoinDenomination denomination, int quantity)
    {
        Denomination = denomination;
        Quantity = quantity;
    }
}