using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services.Implementatios
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderFromCartAsync(int userId, string shippingAddress, string paymentMethod)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
                throw new Exception("Cart is empty or not found.");

            foreach (var item in cart.Items)
            {
                if (item.Product.StockQuantity < item.Quantity)
                    throw new Exception($"Insufficient stock for product: {item.Product.Name}");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress,
                // PaymentMethod = paymentMethod,
                // PaymentStatus = "Pending",
                Status = OrderStatus.Pending,
                TotalAmount = cart.Items.Sum(ci => ci.Quantity * ci.UnitPrice),
                Items = cart.Items.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                }).ToList()
            };

            foreach (var item in cart.Items)
            {
                var product = item.Product;
                product.StockQuantity -= item.Quantity;
                _context.Products.Update(product);
            }

            _context.Orders.Add(order);
            await _cartService.ClearCartAsync(cart.Id);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new Exception("Order not found.");

            order.Status = status;
            await _context.SaveChangesAsync();
        }

    }
}
