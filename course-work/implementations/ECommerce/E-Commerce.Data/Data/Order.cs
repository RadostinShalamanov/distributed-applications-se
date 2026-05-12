using E_Commerce.Data.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }


        [MaxLength(20)]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        [MaxLength(80)]
        public string Address { get; set; }

        public bool IsPaid { get; set; } = false;


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
