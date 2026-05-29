using System.ComponentModel.DataAnnotations;

namespace E_Commerce.FrontEnd.Models.Users
{
    public class UserRequestModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
