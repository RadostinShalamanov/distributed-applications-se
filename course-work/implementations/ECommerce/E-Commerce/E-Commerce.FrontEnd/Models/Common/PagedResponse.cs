namespace E_Commerce.FrontEnd.Models.Common
{
    public class PagedResponse<T>
    {
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public List<T> Items { get; set; } = new List<T>();
    }
}
