using Lab2.RpgInventory.Interfaces;

namespace Lab2.RpgInventory.Strategies;

public class ConsumeStrategies : InterfaceUseStrategy
{
    public string Execute(InterfaceItem item)
    {
        return $"[Употреблено]: {item.Name}";
    }
}