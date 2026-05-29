using System.Collections.Generic;

namespace E_Commerce.FrontEnd.Models.Orders
{
    public class OrderUpdateModel
    {
        public string Status { get; set; }

        public List<OrderItemUpdateModel> Items { get; set; } = new();
    }
}
