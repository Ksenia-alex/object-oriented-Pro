using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class Menu
{
    private static readonly Lazy<Menu> _instance = new(() => new Menu());
    public static Menu Instance => _instance.Value;
    
    public List<Dish> AvailableDishes { get; }

    public Menu()
    {
        AvailableDishes = new List<Dish>();
    }
}