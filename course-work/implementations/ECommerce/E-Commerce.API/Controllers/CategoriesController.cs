using E_Commerce.API.DTOs.Category;
using E_Commerce.API.DTOs.Product;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IBaseService<Category> _service;

        public CategoriesController(ECommerceDbContext context, IBaseService<Category> service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _service.Add(category);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
