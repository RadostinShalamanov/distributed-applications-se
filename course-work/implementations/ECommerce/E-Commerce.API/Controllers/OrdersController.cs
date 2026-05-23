using E_Commerce.API.DTOs.Order;
using E_Commerce.API.DTOs.Product;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Data.Data.Enums;
using E_Commerce.Repository.Interfaces;
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
    public class OrdersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IOrderService _service;

        public OrdersController(ECommerceDbContext context, IOrderService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders([FromQuery] OrderQueryParameter query)
        {

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int? loggedUserId = int.TryParse(userIdClaim, out var id) ? id : null;
            bool isAdmin = User.IsInRole("Admin");
            var orders = await _service.GetAllOrders(loggedUserId, isAdmin);

            if (!string.IsNullOrEmpty(query.Status) && Enum.TryParse<OrderStatus>(query.Status, true, out var status))
                orders = orders.Where(o => o.Status == status).ToList();

            if (query.IsPaid.HasValue)
                orders = orders.Where(o => o.IsPaid == query.IsPaid.Value).ToList();

            if (query.FromDate.HasValue)
                orders = orders.Where(o => o.OrderDate >= query.FromDate.Value).ToList();

            if (query.ToDate.HasValue)
                orders = orders.Where(o => o.OrderDate <= query.ToDate.Value).ToList();

            orders = query.SortBy?.ToLower() switch
            {
                "date" => query.Descending ? orders.OrderByDescending(o => o.OrderDate).ToList()
                                                 : orders.OrderBy(o => o.OrderDate).ToList(),
                "totalprice" => query.Descending ? orders.OrderByDescending(o => o.TotalPrice).ToList()
                                                 : orders.OrderBy(o => o.TotalPrice).ToList(),
                _ => orders.OrderByDescending(o => o.OrderDate).ToList()
            };

            var totalItems = orders.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            var paged = orders
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    Address = o.Address,
                    TotalPrice = o.TotalPrice,
                    IsPaid = o.IsPaid,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                    Items = o.OrderItems.Select(i => new OrderItemResponseDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
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

        public async Task<ActionResult> GetById(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int? loggedUserId = int.TryParse(userIdClaim, out var userId) ? userId : null;
            bool isAdmin = User.IsInRole("Admin");
            var order = await _service.GetOrderById(id, loggedUserId, isAdmin);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order id #{id} not found.");
            }



            var response = new OrderResponseDto
            {
                Id = order.Id,
                Address = order.Address,
                TotalPrice = order.TotalPrice,
                IsPaid = order.IsPaid,
                Status = order.Status,
                OrderDate = order.OrderDate,
                Items = order.OrderItems.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequestDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            int loggedUserId = int.Parse(userIdClaim);
            bool isAdmin = User.IsInRole("Admin");



            var order = new Order
            {
                UserId = isAdmin ? dto.UserId : loggedUserId,
                OrderDate = dto.OrderDate,
                Address = dto.Address,
                IsPaid = false,
                Status = OrderStatus.Pending,
                OrderItems = dto.Items.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };

            await _service.CreateOrder(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, OrderUpdateDto dto)
        {
            var toUpdate = await _service.GetById(id);
            if (toUpdate == null)
            {
                throw new KeyNotFoundException($"Order id #{id} not found.");
            }

            toUpdate.OrderDate = dto.OrderDate;
            toUpdate.TotalPrice = dto.TotalPrice;

            await _service.Update(toUpdate);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{orderId}/total-price")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalPriceByOrderId(int orderId)
        {
            var totalPrice = await _service.GetOrderPriceById(orderId);
            return Ok(new OrderTotalPriceResponseDto
            {
                OrderId = orderId,
                TotalPrice = totalPrice,
            });
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            await _service.UpdateStatus(id, dto.Status);
            return Ok(new
            {
                Message = "Order status updated"
            });
        }

        [HttpPatch("{orderId}/pay")]

        public async Task<IActionResult> PayOrder(int orderId, [FromBody] UpdatePaymentDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int? loggedUserId = int.TryParse(userIdClaim, out var id) ? id : null;
            bool isAdmin = User.IsInRole("Admin");
            await _service.PayOrder(orderId, dto.Amount, loggedUserId, isAdmin);

            return Ok(new
            {
                Message = "Order paid successfully"
            });
        }
    }
}
