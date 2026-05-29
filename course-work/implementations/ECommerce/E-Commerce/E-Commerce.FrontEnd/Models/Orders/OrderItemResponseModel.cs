namespace E_Commerce.FrontEnd.Models.Orders
{
    public class OrderItemResponseModel
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
