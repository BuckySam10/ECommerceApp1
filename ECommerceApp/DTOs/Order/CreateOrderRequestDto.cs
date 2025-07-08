using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        [StringLength(500)]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
    }
}