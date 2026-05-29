using E_Commerce.FrontEnd.Models.Categories;
using E_Commerce.FrontEnd.Models.Common;
using E_Commerce.FrontEnd.Models.Products;
using E_Commerce.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace E_Commerce.FrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _service;
        private readonly CategoryApiService _categoryService;

        public ProductsController(ProductApiService service, CategoryApiService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? search, decimal? minPrice, decimal? maxPrice, int? categoryId, string? sortBy, bool descending = false, int page = 1)
        {
            var query = $"api/products?page={page}&pageSize=6";
            if (!string.IsNullOrEmpty(search)) query += $"&search={search}";
            if (minPrice.HasValue) query += $"&minPrice={minPrice}";
            if (maxPrice.HasValue) query += $"&maxPrice={maxPrice}";
            if (categoryId.HasValue) query += $"&categoryId={categoryId}";
            if (!string.IsNullOrEmpty(sortBy)) query += $"&sortBy={sortBy}&descending={descending}";

            var response = await _service.GetProducts(query);
            if (response == null)
            {
                // unauthorized -> redirect to login
                return RedirectToAction("Login", "Account");
            }

            // Fetch categories to map category names
            var categories = await _categoryService.GetCategories();
            var categoryMap = categories?.Items?.ToDictionary(c => c.Id, c => c.Name) ?? new Dictionary<int, string>();

            // Map category names to products
            foreach (var product in response.Items)
            {
                if (categoryMap.TryGetValue(product.CategoryId, out var categoryName))
                {
                    product.CategoryName = categoryName;
                }
            }
            ViewBag.Search = search;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = categories?.Items ?? new List<CategoryResponseModel>();
            ViewBag.SortBy = sortBy;
            ViewBag.Descending = descending;

            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch categories to map category name
            if (product.CategoryId > 0)
            {
                var categories = await _categoryService.GetCategories();
                var category = categories?.Items?.FirstOrDefault(c => c.Id == product.CategoryId);
                if (category != null)
                {
                    product.CategoryName = category.Name;
                }
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesDropdown();
            return View(new ProductRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestModel dto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropdown();
                return View(dto);
            }
            var token = HttpContext.Session.GetString("JWToken");

            await _service.CreateProduct(dto, token);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(
            int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null) return RedirectToAction("Login", "Account");

            var dto = new ProductUpdateModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };

            // --- FIX 3: Also required for the Edit View ---
            await PopulateCategoriesDropdown();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            ProductUpdateModel dto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropdown();
                return View(dto);
            }

            var token = HttpContext.Session.GetString("JWToken");
            await _service.UpdateProduct(id, dto, token);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");

            await _service.DeleteProduct(id, token);

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCategoriesDropdown()
        {
            var categories = await _categoryService.GetCategories();
            var categoryList = categories?.Items ?? new List<CategoryResponseModel>();

            // Standard MVC approach for dropdowns. 
            // If your view expects a raw List instead of a SelectList, just use: ViewBag.Categories = categoryList;
            ViewBag.Categories = new SelectList(categoryList, "Id", "Name");
        }
    }
}
