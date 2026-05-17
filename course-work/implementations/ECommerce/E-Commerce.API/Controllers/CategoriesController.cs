using E_Commerce.API.DTOs.Category;
using E_Commerce.API.DTOs.Product;
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
            var categories = await _service.GetAll(x => x.Products);
            return Ok(categories.Select(x => new CategoryResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Products = x.Products.Select(s => new ProductCategoryResponseDto
                {
                    Name = s.Name,
                    Description = s.Description
                }).ToList()
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CategoryRequestDto dto)
        {
            var toUpdate = await _service.GetById(id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Name = dto.Name;
            toUpdate.Description = dto.Description;

            await _service.Update(toUpdate);
            return Ok();
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

    }
}
