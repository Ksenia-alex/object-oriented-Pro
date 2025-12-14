using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class DiscountCostStrategy : InterfaceCostCalculationStrategy
{
    private const decimal DeliveryFee = 10.0m;
    private const decimal Discount = 0.5m;

    public decimal CalculateCost(Order order)
    {
        decimal total = order.Dishes.Sum(dish => dish.Price);
        total *= Discount;
        total += DeliveryFee;
        return total;
    }
}