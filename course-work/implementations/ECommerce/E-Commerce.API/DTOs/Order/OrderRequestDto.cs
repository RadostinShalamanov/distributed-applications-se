namespace E_Commerce.API.DTOs.Order
{
    public class OrderRequestDto
    {
        public int UserId { get; set; }

        public string Address { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItemRequestDto> Items { get; set; }
    }
}
