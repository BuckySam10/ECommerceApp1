namespace ECommerceApp.DTOs.Cart
{
    public class CartResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}