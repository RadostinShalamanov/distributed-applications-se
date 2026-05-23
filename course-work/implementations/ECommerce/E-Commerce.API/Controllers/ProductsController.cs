using E_Commerce.API.DTOs.Product;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts([FromQuery] ProductQueryParameters query)
        {
            //var products = await _service.GetAll(x => x.Category);
            //return Ok(products.Select(x => new ProductResponseDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Price = x.Price,
            //    Description = x.Description,
            //    Category = x.Category.Name
            //}));
            var products = await _dbContext.Products
            .Include(p => p.Category)
            .AsQueryable()
            .ToListAsync();

            // Filtering
            if (!string.IsNullOrEmpty(query.Search))
                products = products.Where(p => p.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (query.MinPrice.HasValue)
                products = products.Where(p => p.Price >= query.MinPrice.Value).ToList();

            if (query.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= query.MaxPrice.Value).ToList();

            if (query.CategoryId.HasValue)
                products = products.Where(p => p.CategoryId == query.CategoryId.Value).ToList();

            // Sorting
            products = query.SortBy?.ToLower() switch
            {
                "price" => query.Descending ? products.OrderByDescending(p => p.Price).ToList()
                                            : products.OrderBy(p => p.Price).ToList(),
                "name" => query.Descending ? products.OrderByDescending(p => p.Name).ToList()
                                            : products.OrderBy(p => p.Name).ToList(),
                _ => products.OrderBy(p => p.Id).ToList()
            };

            // Paging
            var totalItems = products.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            var paged = products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category?.Name
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

        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _service.GetById(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product id #{id} not found.");
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
                throw new KeyNotFoundException($"Product id #{id} not found.");
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
