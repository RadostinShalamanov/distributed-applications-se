namespace E_Commerce.FrontEnd.Models.Orders
{
    public class OrderItemUpdateModel
    {
     

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
