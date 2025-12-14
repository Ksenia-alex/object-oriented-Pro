using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class DeliveringState(Order order) : InterfaceOrderState
{
    public OrderStatus Status => OrderStatus.Delivering;

    public void AddDish(Dish dish) {}

    public void ChangeStatus() => order.SetState(new CompletedState(order));

    public void Cancel() {}
}