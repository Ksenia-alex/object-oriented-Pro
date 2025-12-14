using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Interfaces;

public interface InterfaceOrderObserver
{
    void UpdateOrder(Order order);
}