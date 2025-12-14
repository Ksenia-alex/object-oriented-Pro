using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;
using Lab3.DeliverySystem.Enums;

namespace Lab3.DeliverySystem.Services;

public class PreparingState(Order order) : InterfaceOrderState
{
    public OrderStatus Status => OrderStatus.Preparing;
    
    public void AddDish(Dish dish) => order.Dishes.Add(dish);

    public void ChangeStatus() => order.SetState(new DeliveringState(order));

    public void Cancel() => order.SetState(new CancelledState(order));
}