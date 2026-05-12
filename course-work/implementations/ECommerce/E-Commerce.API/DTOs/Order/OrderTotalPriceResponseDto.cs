namespace E_Commerce.API.DTOs.Order
{
    public class OrderTotalPriceResponseDto
    {
        public int OrderId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
