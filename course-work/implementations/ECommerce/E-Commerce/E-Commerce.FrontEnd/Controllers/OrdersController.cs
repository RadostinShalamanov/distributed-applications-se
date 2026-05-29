using E_Commerce.FrontEnd.Helpers;
using E_Commerce.FrontEnd.Models.Orders;
using E_Commerce.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Commerce.FrontEnd.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderApiService _service;

        public OrdersController(OrderApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? status, int? userId, bool? isPaid, string? sortBy, bool descending = false, int page = 1)
        {
            var query = $"api/orders?page={page}&pageSize=10";
            if (!string.IsNullOrEmpty(status)) query += $"&status={status}";
            if (userId.HasValue) query += $"&userId={userId}";
            if (isPaid.HasValue) query += $"&isPaid={isPaid}";
            if (!string.IsNullOrEmpty(sortBy)) query += $"&sortBy={sortBy}&descending={descending}";

            var response = await _service.GetOrders(query);
            if (response == null) return RedirectToAction("Login", "Account");

            ViewBag.Status = status;
            ViewBag.UserId = userId;
            ViewBag.IsPaid = isPaid;
            ViewBag.SortBy = sortBy;
            ViewBag.Descending = descending;

            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _service.GetOrderById(id);
            if (order == null) return NotFound();

            ViewBag.IsAdmin = HttpContext.IsAdmin();
            return View(order);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequestModel dto)
        {
            if (dto.UserId == 0)
            {
              
                dto.UserId = HttpContext.Session.GetInt32("CurrentUserId") ?? 0;
            }

            await _service.CreateOrder(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!HttpContext.IsAdmin()) return Forbid();
            var order = await _service.GetOrderById(id);
            var dto = new OrderUpdateModel
            {
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItemUpdateModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TotalPrice = i.Price
                }).ToList()
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderUpdateModel dto)
        {
            if (!HttpContext.IsAdmin()) return Forbid();

            await _service.UpdateOrder(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HttpContext.IsAdmin()) return Forbid();
            await _service.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int id)
        {
            var order = await _service.GetOrderById(id);

            if (order == null) return NotFound();
            if (order.IsPaid)
            {
                TempData["Info"] = "This order is already paid.";
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int id, decimal amount)
        {
            var success = await _service.PayOrder(id, amount);

            if (success)
            {
                TempData["Success"] = $"Payment of {amount:C} for Order #{id} was successful! Your order is now being delivered.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Payment failed. Please check your balance and try again.";
            var order = await _service.GetOrderById(id);
            return View(order);
        }
    }
}
