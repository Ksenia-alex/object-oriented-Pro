using Lab2.RpgInventory.Interfaces;
namespace Lab2.RpgInventory.Strategies;

public class EquipmentStrategies : InterfaceUseStrategy
{
    public string Execute(InterfaceItem item)
    {
        return $"[Экипировано]:  {item.Name}";
    }
}