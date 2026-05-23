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
    [Authorize(Roles = "Admin")]
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

        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers([FromQuery] UserQueryParameter query)
        {
            var users = await _service.GetUsers();


            if (!string.IsNullOrEmpty(query.Search))
                users = users.Where(u => u.Username.Contains(query.Search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(query.Email))
                users = users.Where(u => u.Email.Contains(query.Email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(query.Role))
                users = users.Where(u => u.Role.Contains(query.Role, StringComparison.OrdinalIgnoreCase)).ToList();


            users = query.SortBy?.ToLower() switch
            {
                "username" => query.Descending ? users.OrderByDescending(u => u.Username).ToList()
                                               : users.OrderBy(u => u.Username).ToList(),
                "email" => query.Descending ? users.OrderByDescending(u => u.Email).ToList()
                                               : users.OrderBy(u => u.Email).ToList(),
                "role" => query.Descending ? users.OrderByDescending(u => u.Role).ToList()
                                               : users.OrderBy(u => u.Role).ToList(),
                _ => users.OrderBy(u => u.Id).ToList()
            };


            var totalItems = users.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            var paged = users.
                Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new UserResponseDto
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
                        IsPaid = o.IsPaid,
                        Status = o.Status,
                        Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                        {
                            ProductId = oi.ProductId,
                            ProductName = oi.Product.Name,
                            Quantity = oi.Quantity,
                            Price = oi.Price
                        }).ToList()
                    }).ToList()
                });

            return Ok(new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = query.Page,
                PageSize = query.PageSize,
                Items = paged
            });
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _service.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id #{id} not found.");
            }


            var response = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                Orders = user.Orders.Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    Address = o.Address,
                    TotalPrice = o.TotalPrice,
                    IsPaid = o.IsPaid,
                    Status = o.Status,
                    Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
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
        public async Task<IActionResult> Update(int id, UserRequestDto dto)
        {
            var user = await _service.GetById(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User id #{id} not found.");
            }

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;
            user.Role = dto.Role;

            await _service.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully");
        }
    }
}
