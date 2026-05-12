namespace E_Commerce.API.DTOs.User
{
    public class UserRequestDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
    }
}
