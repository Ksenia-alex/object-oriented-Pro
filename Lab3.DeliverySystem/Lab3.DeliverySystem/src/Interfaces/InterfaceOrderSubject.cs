using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Interfaces;

public interface InterfaceOrderSubject
{
    void Attach(InterfaceOrderObserver order);
    void Detach(InterfaceOrderObserver order);
    void Notify();
}