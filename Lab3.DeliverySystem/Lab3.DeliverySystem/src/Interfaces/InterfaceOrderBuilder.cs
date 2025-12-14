using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Interfaces;

public interface InterfaceOrderBuilder
{
    InterfaceOrderBuilder SetCustomer(String customer);
    InterfaceOrderBuilder SetAddress(String address);
    InterfaceOrderBuilder AddDish(Dish dish);
    InterfaceOrderBuilder SetCostCalculationStrategy(InterfaceCostCalculationStrategy strategy);
    Order Build();
}