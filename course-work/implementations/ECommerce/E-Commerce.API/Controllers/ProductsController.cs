using E_Commerce.API.DTOs.Product;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IBaseService<Product> _service;
        public ProductsController(ECommerceDbContext dbContext, IBaseService<Product> service)
        {
            _dbContext = dbContext;
            _service = service;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
        {
            var products = await _service.GetAll(x => x.Category);
            return Ok(products.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Category = x.Category.Name
            }));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _service.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            await _service.Add(product);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ProductRequestDto dto)
        {
            var toUpdate = await _service.GetById(id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Name = dto.Name;
            toUpdate.Price = dto.Price;
            toUpdate.Description = dto.Description;

            await _service.Update(toUpdate);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
