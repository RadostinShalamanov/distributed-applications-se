using E_Commerce.API.DTOs.Paging;

namespace E_Commerce.API.DTOs.User
{
    public class UserQueryParameter : QueryParameters
    {
        public string? Role { get; set; }

        public string? Email { get; set; }
    }
}
