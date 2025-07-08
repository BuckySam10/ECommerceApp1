using ECommerceApp.Models;

namespace ECommerceApp.Services.Interfaces
{
    public interface ICartService
    {
       Task <Cart> GetCartByUserIdAsync(int userId);
        Task AddItemToCartAsync(int userId, int productId, int quantity);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveItemFromCartAsync(int cartItemId);
        Task ClearCartAsync(int cartId);
    }
}
