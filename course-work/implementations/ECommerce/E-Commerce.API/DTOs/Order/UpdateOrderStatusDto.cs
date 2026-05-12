using E_Commerce.Data.Data.Enums;

namespace E_Commerce.API.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public OrderStatus Status { get; set; }
    }
}
