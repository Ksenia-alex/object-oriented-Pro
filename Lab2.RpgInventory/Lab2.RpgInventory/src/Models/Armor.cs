using Lab2.RpgInventory.Interfaces;
using Lab2.RpgInventory.Strategies;

namespace Lab2.RpgInventory.Models;

public class Armor : Item, InterfaceUpgradable
{
    public int Defense { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxLevel { get; }

    public Armor(string name, string description, int weight, int value, int maxLevel, int defense)
        : base(name, description, ItemType.Armor, weight, value, new EquipmentStrategies())
    {
        Defense = defense;
        MaxLevel = maxLevel;
    }

    public void Upgrade(int amount)
    {
        if (Level < MaxLevel) Level++;
        Defense += amount;
        Value += (amount * 5);
    }

    public override string GetItemDetails()
    {
        var baseInfo = base.GetItemDetails();
        return baseInfo + $"Защита: {Defense}, Уровень: {Level}/{MaxLevel}";
    }
}