using Lab3.DeliverySystem.Enums;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Interfaces;

public interface InterfaceOrderState
{
    OrderStatus Status { get; }
    void AddDish(Dish dish);
    void ChangeStatus();
    void Cancel();
}