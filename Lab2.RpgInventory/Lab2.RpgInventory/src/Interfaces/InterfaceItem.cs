using Lab2.RpgInventory.Models;

namespace Lab2.RpgInventory.Interfaces;

public interface InterfaceItem
{
    string Name { get; }
    string Description { get; }
    ItemType Type { get; }
    int Weight { get; }
    int Value { get; }
    string Use();
    string GetItemDetails();
}