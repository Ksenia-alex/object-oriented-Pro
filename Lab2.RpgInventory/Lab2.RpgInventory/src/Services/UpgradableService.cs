using Lab2.RpgInventory.Interfaces;

namespace Lab2.RpgInventory.Services;

public class UpgradableService
{
    public bool Improved(InterfaceItem? item, int amount)
    {
        if (item is null) return false;

        if (item is InterfaceUpgradable upgradableItem)
        {
            upgradableItem.Upgrade(amount);
            return true;
        }
        
        return false;
    }
}