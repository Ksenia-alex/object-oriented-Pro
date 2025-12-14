namespace Lab3.DeliverySystem.Models;

public class Dish
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Dish(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}