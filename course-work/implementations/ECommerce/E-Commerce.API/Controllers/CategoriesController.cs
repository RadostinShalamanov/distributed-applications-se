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

        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories([FromQuery] CategoryQueryParameter query)
        {
            var categories = await _service.GetAll(x => x.Products);

            if (!string.IsNullOrEmpty(query.Search))
                categories = categories.Where(c => c.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(query.Name))
                categories = categories.Where(c => c.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase)).ToList();

            categories = query.SortBy?.ToLower() switch
            {
                "name" => query.Descending ? categories.OrderByDescending(c => c.Name).ToList()
                                           : categories.OrderBy(c => c.Name).ToList(),
                _ => categories.OrderBy(c => c.Id).ToList()
            };

            var totalItems = categories.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            var paged = categories
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new CategoryResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Products = x.Products.Select(s => new ProductCategoryResponseDto
                    {
                        Name = s.Name,
                        Description = s.Description
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
                throw new KeyNotFoundException($"Category id #{id} is not found .");
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
