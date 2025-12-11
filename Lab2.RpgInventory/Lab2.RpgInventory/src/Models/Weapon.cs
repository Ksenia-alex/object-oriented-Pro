using Lab2.RpgInventory.Interfaces;
using Lab2.RpgInventory.Strategies;

namespace Lab2.RpgInventory.Models;

public class Weapon : Item, InterfaceUpgradable
{
    public int Damage { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxLevel { get; }

    public Weapon(string name, string description, int weight, int value, int maxLevel, int damage)
        : base(name, description, ItemType.Weapon, weight, value, new EquipmentStrategies())
    {
        Damage = damage;
        MaxLevel = maxLevel;
    }

    public void Upgrade(int amount)
    {
        if (Level < MaxLevel) Level++;
        Damage += amount;
        Value += (amount * 10);
    }

    public override string GetItemDetails()
    {
        var baseInfo = base.GetItemDetails();
        return baseInfo + $"Урон: {Damage}, Уровень: {Level}/{MaxLevel}";
    }
}