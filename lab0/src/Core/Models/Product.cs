namespace object_oriented_Pro.lab0.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; }
    public decimal Price
    {
        get => _price;
        set => _price = value >= 0 
            ? value 
            : throw new AggregateException("Price cannot be negative");
    }

    public int Quantity
    {
        get => _quantity; 
        set => _quantity = value >= 0 
            ? value 
            : throw new AggregateException("Quantity cannot be negative");
    }
    
    private decimal _price;
    private int _quantity;

    public Product(int id, string name, decimal price, int quantity)
    {
        Id = id;
        Name = string.IsNullOrWhiteSpace(name)
            ? throw new AggregateException("Name cannot be empty")
            : name;
        Price = price;
        Quantity = quantity;
    }
}