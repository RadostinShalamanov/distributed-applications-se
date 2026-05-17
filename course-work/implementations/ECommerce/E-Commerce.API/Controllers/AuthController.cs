using E_Commerce.API.DTOs.Auth;
using E_Commerce.API.Helpers;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {


        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;


        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fail(ModelState));

            if (await _userService.ExistsAsync(dto.Email))
            {
                ModelState.AddModelError("Email", $"An account with '{dto.Email}' already exists.");
                return Conflict(ApiResponse<object>.Fail(ModelState));

            }

            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password
            };

            await _userService.Add(newUser);


            var token = _jwtService.GetJwt(newUser);
            return CreatedAtAction(nameof(Login), ApiResponse<object>.Ok(new { Token = token }));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Fail(ModelState));

            var user = await _userService.GetByEmail(dto.Email);


            if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
            {
                ModelState.AddModelError("Credentials", "Invalid email or password.");
                return Unauthorized(ApiResponse<object>.Fail(ModelState));
            }

            var token = _jwtService.GetJwt(user);


            return Ok(ApiResponse<object>.Ok(new { Token = token }));
        }
    }
}
