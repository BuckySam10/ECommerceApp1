using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.Cart
{
    public class UpdateCartItemRequestDto
    {
        [Required]
        public int CartItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}