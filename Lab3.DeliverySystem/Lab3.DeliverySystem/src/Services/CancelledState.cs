using System.Diagnostics.Contracts;
using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class CancelledState(Order order) : InterfaceOrderState
{
    public OrderStatus Status => OrderStatus.Cancelled;
    public void AddDish(Dish dish) {}
    public void ChangeStatus() {}
    public void Cancel() {}
    
}