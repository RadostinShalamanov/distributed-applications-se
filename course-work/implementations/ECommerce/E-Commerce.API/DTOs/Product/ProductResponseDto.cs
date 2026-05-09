using E_Commerce.Data.Data;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
//public int Id { get; set; }

//[Required]
//[MaxLength(50)]

//public string Name { get; set; }

//[MaxLength(200)]

//public string Description { get; set; }

//[Required]
//public decimal Price { get; set; }

//public int Quantity { get; set; }

//[Required]
//public int CategoryId { get; set; }
//public Category Category { get; set; }

//public double Rating { get; set; }

//public ICollection<OrderItem> OrderItems { get; set; }