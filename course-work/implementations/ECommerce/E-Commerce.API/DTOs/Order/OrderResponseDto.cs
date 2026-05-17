using E_Commerce.Data.Data.Enums;

namespace E_Commerce.API.DTOs.Order
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public bool IsPaid { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderItemResponseDto> Items { get; set; }
    }
}
