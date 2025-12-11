using Lab2.RpgInventory.Interfaces;
using Lab2.RpgInventory.Strategies;

namespace Lab2.RpgInventory.Models;

public class Potion : Item
{
    public int HealAmount { get; }
    
    public Potion(string name, int weight, int value, int healAmount)
        : base(name, $"Востанавливает {healAmount} HP",  ItemType.Potion, weight, value, new ConsumeStrategies())
        {
            HealAmount = healAmount;
        }
}
