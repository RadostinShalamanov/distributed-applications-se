using System.Collections.Generic;

namespace E_Commerce.FrontEnd.Models.Orders
{
    public class OrderRequestModel
    {
        public int UserId { get; set; }

        public string Address { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItemRequestModel> Items { get; set; } = new();
    }


}
