using E_Commerce.FrontEnd.Models.Users;
using E_Commerce.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.FrontEnd.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserApiService _service;

        public UsersController(UserApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? email, string? role, string? sortBy, bool descending = false, int page = 1)
        {
            var query = $"api/users?page={page}&pageSize=10";
            if (!string.IsNullOrEmpty(search)) query += $"&search={search}";
            if (!string.IsNullOrEmpty(email)) query += $"&email={email}";
            if (!string.IsNullOrEmpty(role)) query += $"&role={role}";
            if (!string.IsNullOrEmpty(sortBy)) query += $"&sortBy={sortBy}&descending={descending}";

            var response = await _service.GetUsers(query);
            if (response == null) return RedirectToAction("Login", "Account");

            ViewBag.Search = search;
            ViewBag.Email = email;
            ViewBag.Role = role;
            ViewBag.SortBy = sortBy;
            ViewBag.Descending = descending;

            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _service.GetUserById(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserRequestModel dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _service.CreateUser(dto, token);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _service.GetUserById(id);
            var dto = new UserUpdateModel
            {
                Username = user.Username,
                Email=user.Email,
                Role = user.Role
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserUpdateModel dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _service.UpdateUser(id, dto, token);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _service.DeleteUser(id, token);
            return RedirectToAction(nameof(Index));
        }
    }
}
