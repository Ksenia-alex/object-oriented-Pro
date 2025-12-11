using Lab2.RpgInventory.Models;
using Lab2.RpgInventory.Services;
using Xunit;

namespace Lab2.RpgInventory.tests.ServicesTests;

public class UpgradableServiceTests
{
    private readonly UpgradableService _upgradableService;
    private readonly Armor _armor;
    private readonly Potion _potion;
    private readonly QuestItem _questItem;
    private readonly Weapon _weapon;

    public UpgradableServiceTests()
    {
        _upgradableService = new UpgradableService();
        _armor = new Armor
        (
            name: "Test Armor",
            description: "Description",
            weight: 20,
            value: 100,
            maxLevel: 100,
            defense: 200
        );
        _potion = new Potion
        (
            name: "Test Potion",
            weight: 10,
            value: 80,
            healAmount: 40
        );
        _questItem = new QuestItem
        (
            name: "Test Quest Item",
            description: "Description",
            weight: 20
        );
        _weapon = new Weapon
        (
            name: "Test Weapon",
            description: "Description",
            weight: 100,
            value: 100,
            maxLevel: 2,
            damage: 300
        );
    }

    [Fact]
    public void ImprovedArmor_ShouldUpgradeArmor()
    {
        var level = _armor.Level;
        var value = _armor.Value;
        var defense = _armor.Defense;

        bool status = _upgradableService.Improved(_armor, 20);

        Assert.Equal(status, true);
        Assert.Equal(_armor.Level, level + 1);
        Assert.Equal(_armor.Value, value + 100);
        Assert.Equal(_armor.Defense, defense + 20);
    }
    
    [Fact]
    public void ImprovedPotion_ShouldReturnFalse()
    {
        bool status = _upgradableService.Improved(_potion, 20);
        
        Assert.Equal(status, false);
    }
    
    [Fact]
    public void ImprovedQuestItem_ShouldReturnFalse()
    {
        bool status = _upgradableService.Improved(_questItem, 20);
        
        Assert.Equal(status, false);
    }
    
    [Fact]
    public void ImprovedWeapon_ShouldUpgradeWeapon()
    {
        var level = _weapon.Level;
        var value = _weapon.Value;
        var damage = _weapon.Damage;

        bool status = _upgradableService.Improved(_weapon, 20);

        Assert.Equal(status, true);
        Assert.Equal(_weapon.Level, level + 1);
        Assert.Equal(_weapon.Value, value + 200);
        Assert.Equal(_weapon.Damage, damage + 20);
    }

}