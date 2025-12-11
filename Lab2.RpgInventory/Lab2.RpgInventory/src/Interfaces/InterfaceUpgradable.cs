namespace Lab2.RpgInventory.Interfaces;

public interface InterfaceUpgradable
{
    int Level { get; }
    void Upgrade(int amount);
}