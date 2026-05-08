using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Data.Data
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}