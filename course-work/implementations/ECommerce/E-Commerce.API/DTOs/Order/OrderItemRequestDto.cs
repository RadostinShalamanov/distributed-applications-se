namespace E_Commerce.API.DTOs.Order
{
    public class OrderItemRequestDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}