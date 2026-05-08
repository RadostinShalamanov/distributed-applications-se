using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Data.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}