namespace Lab3.DeliverySystem.Models;

public class Dish
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Dish(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}