using E_Commerce.API.DTOs.Order;
using E_Commerce.API.DTOs.Product;
using E_Commerce.API.DTOs.User;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IUserService _service;

        public UsersController(ECommerceDbContext context,
            IUserService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _service.GetUsers();

            return Ok(users.Select(x => new UserResponseDto
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                CreatedAt = x.CreatedAt,
                Role = x.Role,
                Orders = x.Orders.Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    Address = o.Address,
                    TotalPrice = o.TotalPrice,
                    Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                }).ToList()
            }));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }


            var response = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUser([FromBody] UserRequestDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash),
                Role = dto.Role
            };

            await _service.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UserRequestDto dto)
        {
            var user = await _service.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;
            user.Role = dto.Role;

            await _service.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully");
        }
    }
}
