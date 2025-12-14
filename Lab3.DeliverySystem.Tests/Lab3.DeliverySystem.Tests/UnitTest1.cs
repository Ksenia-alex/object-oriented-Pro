using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;
using Lab3.DeliverySystem.Services;

namespace Lab3.DeliverySystem.Tests;

public class UnitTest1
{
    private readonly InterfaceOrderBuilder _builder;
    private readonly Dish _pizza;
    
    public UnitTest1()
    {
        _builder = new OrderBuilder();
        _pizza = Menu.Instance.AvailableDishes[0];
    }

    [Fact]
    public void Builder_ShouldCreateOrderCorrectly()
    {
        var order = _builder
            .SetCustomer("Иван")
            .SetAddress("ул. Пушкина")
            .AddDish(_pizza)
            .Build();
        
        Assert.Equal("Иван", order.CustomerName);
        Assert.Single(order.Dishes);
        Assert.Equal(_pizza.Name, order.Dishes.First().Name);
    }

    [Fact]
    public void State_ShouldChangeBehaviorAndTrackStatus()
    {
        var order = _builder.SetCustomer("Анна").SetAddress("ул. Лермонтова, 2").Build();
        Assert.Equal(OrderStatus.Preparing, order.GetStatus());
        
        order.AddDish(_pizza);
        Assert.Single(order.Dishes);
        
        order.ChangeStatus();
        Assert.Equal(OrderStatus.Delivering, order.GetStatus());
        
        order.AddDish(new Dish("Test Dish", 12m)); 
        Assert.Single(order.Dishes);
    }

    [Fact]
    public void Strategy_ShouldCalculateCostDifferently()
    {
        var order = _builder.SetCustomer("Иван").SetAddress("...").AddDish(_pizza).Build();
        
        order.CostCalculationStrategy = new StandartCostStrategy();
        Assert.Equal(25.50m, order.CalculateTotalCost());
        
        order.CostCalculationStrategy = new DiscountCostStrategy();
        Assert.Equal(17.75m, order.CalculateTotalCost());
    }

    [Fact]
    public void Singleton_ShouldReturnSameMenuInstance()
    {
        var menu1 = Menu.Instance;
        var menu2 = Menu.Instance;
        
        Assert.Same(menu1, menu2);
    }
    
    [Fact]
    public void Observer_ShouldBeNotifiedOnStateChange()
    {
        var order = _builder.SetCustomer("Петр").SetAddress("...").Build();
        var notifier = new TestNotifier();
        order.Attach(notifier);
        
        order.ChangeStatus(); 
        
        Assert.True(notifier.WasNotified);
        Assert.Equal(OrderStatus.Delivering, notifier.LastStatus);
    }
    
    private class TestNotifier : InterfaceOrderObserver
    {
        public bool WasNotified { get; private set; }
        public OrderStatus LastStatus { get; private set; }

        public void UpdateOrder(Order order)
        {
            WasNotified = true;
            LastStatus = order.GetStatus();
        }
    }
}