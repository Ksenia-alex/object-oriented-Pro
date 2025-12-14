using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class CompletedState(Order older) : InterfaceOrderState
{
    public OrderStatus Status => OrderStatus.Completed;
    public void AddDish(Dish dish) {}
    public void ChangeStatus() {}
    public void Cancel() {}
}