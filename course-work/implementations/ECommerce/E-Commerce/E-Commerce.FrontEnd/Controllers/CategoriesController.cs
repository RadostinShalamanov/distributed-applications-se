using E_Commerce.FrontEnd.Models.Categories;
using E_Commerce.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Xml.Linq;

namespace E_Commerce.FrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _service;

        public CategoriesController(CategoryApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? name, string? sortBy, bool descending = false, int page = 1)
        {
            var query = $"api/categories?page={page}&pageSize=10";
            if (!string.IsNullOrEmpty(search)) query += $"&search={search}";
            if (!string.IsNullOrEmpty(name)) query += $"&name={name}";
            if (!string.IsNullOrEmpty(sortBy)) query += $"&sortBy={sortBy}&descending={descending}";

            var response = await _service.GetCategories(query);
            if (response == null) return RedirectToAction("Login", "Account");

            ViewBag.Search = search;
            ViewBag.Name = name;
            ViewBag.SortBy = sortBy;
            ViewBag.Descending = descending;

            return View(response);
        }


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestModel dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _service.CreateCategory(dto, token);
            return RedirectToAction(nameof(Index));
        }

       
       

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _service.DeleteCategory(id, token);
            return RedirectToAction(nameof(Index));
        }
    }
}
