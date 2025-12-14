using Lab3.DeliverySystem.Interfaces;
using Lab3.DeliverySystem.Models;

namespace Lab3.DeliverySystem.Services;

public class OrderObserver : InterfaceOrderObserver
{
    public void UpdateOrder(Order order)
    {
        Console.WriteLine($"Статус заказа изменился на {order.GetStatus()}");
    }
}