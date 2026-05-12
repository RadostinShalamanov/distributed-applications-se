using E_Commerce.API.DTOs.Order;
using E_Commerce.API.DTOs.Product;
using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Data.Data.Enums;
using E_Commerce.Repository.Interfaces;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            var orders = await _service.GetAllOrders();

            return Ok(orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                UserId = o.UserId,
                Address = o.Address,
                TotalPrice = o.TotalPrice,
                IsPaid = o.IsPaid,
                Status = o.Status,
                OrderDate = o.OrderDate,
                Items = o.OrderItems.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetById(int id)
        {
            var order = await _service.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] OrderRequestDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
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

        public async Task<IActionResult> Update(int id, OrderUpdateDto dto)
        {
            var toUpdate = await _service.GetById(id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.OrderDate = dto.OrderDate;
            toUpdate.TotalPrice = dto.TotalPrice;

            await _service.Update(toUpdate);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{orderId}/total-price")]

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
            await _service.PayOrder(orderId, dto.Amount);

            return Ok(new
            {
                Message = "Order paid successfully"
            });
        }
    }
}
