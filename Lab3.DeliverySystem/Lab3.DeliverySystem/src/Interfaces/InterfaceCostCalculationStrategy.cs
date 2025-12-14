using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Interfaces;

public interface InterfaceCostCalculationStrategy
{
    decimal CalculateCost(Order order);
}