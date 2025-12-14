using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class StandartCostStrategy : InterfaceCostCalculationStrategy
{
    private const decimal DeliveryFee = 10.0m;

    public decimal CalculateCost(Order order)
    {
        decimal total = order.Dishes.Sum(dish => dish.Price);
        total += DeliveryFee;
        return total;
    }
}