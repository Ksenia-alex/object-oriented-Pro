using Lab2.RpgInventory.Models;
using Lab2.RpgInventory.System;

namespace Lab2.RpgInventory.tests.SystemTests;
using Xunit;

public class InventoryTests
{
    private Inventory _inventory;
    private readonly Weapon _weapon;
    private readonly Armor _armor;
    private readonly Potion _potion;

    public InventoryTests()
    {
        _inventory = new Inventory(100);
        _weapon = new Weapon
        (
            name: "Test Weapon",
            description: "Description",
            weight: 10,
            value: 50,
            maxLevel: 2,
            damage: 20
        );
        _armor = new Armor
        (
            name: "Test Armor",
            description: "Description",
            weight: 101,
            value: 500,
            maxLevel: 100,
            defense: 200
        );
        _potion = new Potion
        (
            name: "Test Potion",
            weight: 1,
            value: 10,
            healAmount: 50
        );
    }
    
    [Fact]
    public void AddItem_ShouldIncreaseWeight()
    {
        _inventory.AddItem(_weapon);

        Assert.Equal(10, _inventory.CurrentCapacity);
    }
    
    [Fact]
    public void AddItem_Overweight_ShouldReturnFalse()
    {
        bool status = _inventory.AddItem(_armor);

        Assert.False(status);
        Assert.Equal(0, _inventory.CurrentCapacity);
    }
    
    [Fact]
    public void Use_Potion_ShouldUseConsumeStrategy()
    {
        _inventory.AddItem(_potion);

        var result = _inventory.UseItem("Test Potion");

        Assert.Contains("[Употреблено]: Test Potion", result);
    }

}