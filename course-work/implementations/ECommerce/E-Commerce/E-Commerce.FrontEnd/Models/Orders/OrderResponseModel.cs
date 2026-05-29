using System;
using System.Collections.Generic;

namespace E_Commerce.FrontEnd.Models.Orders
{
    public class OrderResponseModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsPaid { get; set; }

        public List<OrderItemResponseModel> Items { get; set; } = new();
    }


}
