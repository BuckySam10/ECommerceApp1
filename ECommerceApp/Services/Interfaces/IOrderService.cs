using ECommerceApp.Models;

namespace ECommerceApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderFromCartAsync(int userId, string shippingAddress, string paymentMethod);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
