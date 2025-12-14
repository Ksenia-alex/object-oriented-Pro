using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class OrderBuilder : InterfaceOrderBuilder
{
    private string _customer;
    private string _address;
    private readonly List<Dish> _dishes = new();
    private InterfaceCostCalculationStrategy _strategy = new StandartCostStrategy();

    public InterfaceOrderBuilder SetCustomer(string customer)
    {
        _customer = customer;
        return this;
    }

    public InterfaceOrderBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }

    public InterfaceOrderBuilder AddDish(Dish dish)
    {
        _dishes.Add(dish);
        return this;
    }

    public InterfaceOrderBuilder SetCostCalculationStrategy(InterfaceCostCalculationStrategy strategy)
    {
        _strategy = strategy;
        return this;
    }

    public Order Build()
    {
        var order = new Order(_customer, _address, _strategy);
        _dishes.ForEach(dish => order.AddDish(dish));
        return order;
    }
}