using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Data.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }

        [MaxLength(200)]

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public double Rating { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}