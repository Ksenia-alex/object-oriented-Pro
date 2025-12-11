using System.Text;
using Lab2.RpgInventory.Interfaces;

namespace Lab2.RpgInventory.Models;

public abstract class Item : InterfaceItem
{
    public string Name { get; }
    public string Description { get; }
    public ItemType Type { get; }
    public int Weight { get; }
    public int Value { get; protected set; }

    protected readonly InterfaceUseStrategy UseStrategy;

    protected Item(string name, string description, ItemType type, int weight, int value, InterfaceUseStrategy strategy)
    {
        Name = name;
        Description = description;
        Type = type;
        Weight = weight;
        Value = value;
        UseStrategy = strategy;
    }

    public virtual string Use()
    {
        return UseStrategy.Execute(this);
    }

    public virtual string GetItemDetails()
    {
        return $"[{Type}] {Name}, Описание: {Description}, Вес: {Weight}, Стоимость: {Value}";
    }
}