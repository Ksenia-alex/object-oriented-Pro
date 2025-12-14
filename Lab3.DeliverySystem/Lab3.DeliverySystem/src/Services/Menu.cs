using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class Menu
{
    private static readonly Lazy<Menu> _instance = new(() => new Menu());
    public static Menu Instance => _instance.Value;
    
    public List<Dish> AvailableDishes { get; }

    private Menu()
    {
        AvailableDishes = new List<Dish> {
            new("Пицца Маргарита", 15.50m),
            new("Салат Цезарь",8.00m),
            new("Паста Карбонара", 12.75m)
        };
    }
}