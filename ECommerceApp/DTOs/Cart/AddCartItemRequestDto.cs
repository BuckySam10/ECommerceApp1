using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.Cart
{
    public class AddCartItemRequestDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}