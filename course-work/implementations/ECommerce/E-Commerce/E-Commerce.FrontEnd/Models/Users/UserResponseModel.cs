using E_Commerce.FrontEnd.Models.Orders;

namespace E_Commerce.FrontEnd.Models.Users
{
    public class UserResponseModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public ICollection<OrderResponseModel> Orders { get; set; }
    }
}
