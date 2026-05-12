using E_Commerce.API.DTOs.Product;

namespace E_Commerce.API.DTOs.Category
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductCategoryResponseDto> Products { get; set; }
    }
}
