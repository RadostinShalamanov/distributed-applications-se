using E_Commerce.API.DTOs.Paging;

namespace E_Commerce.API.DTOs.Order
{
    public class OrderQueryParameter : QueryParameters
    {
        public string? Status { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
