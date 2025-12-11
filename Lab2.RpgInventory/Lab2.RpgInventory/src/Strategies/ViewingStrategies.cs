using Lab2.RpgInventory.Interfaces;

namespace Lab2.RpgInventory.Strategies;

public class ViewingStrategies : InterfaceUseStrategy
{
    public string Execute(InterfaceItem item)
    {
        return $"[Просмотр]:  {item.Name}";
    }
}