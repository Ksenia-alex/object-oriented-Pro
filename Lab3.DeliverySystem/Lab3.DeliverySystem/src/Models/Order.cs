using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Services;

namespace Lab3.DeliverySystem.Models;

public class Order : InterfaceOrderSubject
{
    public string CustomerName { get; }
    public string DeliveryAddress { get; }
    public List<Dish> Dishes { get; } = new();
    public InterfaceCostCalculationStrategy CostCalculationStrategy { get; set; }

    private InterfaceOrderState _state;
    private readonly List<InterfaceOrderObserver> _observers = new();

    public Order(string customerName, string deliveryAddress, InterfaceCostCalculationStrategy costCalculationStrategy)
    {
        CustomerName = customerName;
        DeliveryAddress = deliveryAddress;
        CostCalculationStrategy = costCalculationStrategy;
        _state = new PreparingState(this);
    }
    
    public OrderStatus GetStatus() =>  _state.Status;

    public void SetState(InterfaceOrderState state)
    {
        _state = state;
        Notify();
    }
    
    public void AddDish(Dish dish) => _state.AddDish(dish);
    
    public void ChangeStatus() => _state.ChangeStatus();
    
    public void Cancel() => _state.Cancel();
    
    public decimal CalculateTotalCost() => CostCalculationStrategy.CalculateCost(this);
    
    public void Attach(InterfaceOrderObserver observer) => _observers.Add(observer);
    public void Detach(InterfaceOrderObserver observer) => _observers.Remove(observer);
    public void Notify() => _observers.ForEach(order => order.UpdateOrder(this));
}