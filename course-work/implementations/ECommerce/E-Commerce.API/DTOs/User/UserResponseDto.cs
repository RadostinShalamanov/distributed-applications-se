using E_Commerce.API.DTOs.Order;
using E_Commerce.Data.Data;

namespace E_Commerce.API.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderResponseDto> Orders { get; set; }
    }
}
