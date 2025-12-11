using System.Text;
using Lab2.RpgInventory.Interfaces;

using Lab2.RpgInventory.Models;

namespace Lab2.RpgInventory.System;

public class Inventory(int capacity)
{
    private readonly List<InterfaceItem> _items = new List<InterfaceItem>();
    public int Capacity { get; } = capacity;
    
    public int CurrentCapacity => _items.Sum(i => i.Weight);

    public bool AddItem(InterfaceItem item)
    {
        if (CurrentCapacity + item.Weight > Capacity) return false;
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(InterfaceItem item)
    {
        return _items.Remove(item);
    }

    public InterfaceItem? GetItem(string name)
    {
        return _items.FirstOrDefault(i => i.Name == name);
    }

    public string UseItem(string name)
    {
        var item = GetItem(name);
        if (item != null) return item.Use();
        return "Предмет не найден";
    }

    public string GetInventory()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Инвентарь ({CurrentCapacity}/{Capacity}):");
        foreach (var item in _items)
        {
            sb.AppendLine($"-  {item.GetItemDetails()}");
        }
        return sb.ToString().Trim();
    }
}