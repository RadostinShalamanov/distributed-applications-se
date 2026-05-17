using E_Commerce.API.DTOs.Paging;

namespace E_Commerce.API.DTOs.Product
{
    public class ProductQueryParameters: QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
    }
}
