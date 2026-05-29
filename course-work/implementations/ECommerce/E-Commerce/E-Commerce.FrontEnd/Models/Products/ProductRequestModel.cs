using System.ComponentModel.DataAnnotations;

namespace E_Commerce.FrontEnd.Models.Products
{
    public class ProductRequestModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]

        public decimal Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]

        public int CategoryId { get; set; }
    }
}
