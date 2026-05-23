using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Data.Data.Enums;
using E_Commerce.Repository.Interfaces;
using E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly ECommerceDbContext _context;
        private readonly IEmailService _emailService;

        public OrderService(ECommerceDbContext context,
            IBaseRepository<Order> repository,
            IEmailService emailService) : base(repository)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            var user = await _context.Users.FindAsync(order.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            decimal totalPrice = 0;
            foreach (var item in order.OrderItems)
            {

                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with id - {item.ProductId} not found!");
                }

                item.Price = product.Price;
                totalPrice += product.Price * item.Quantity;
            }

            order.TotalPrice = totalPrice;
            _context.Orders.Add(order);

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int? loggedUserId, bool isAdmin)
        {
            var response = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!isAdmin && loggedUserId.HasValue)
            {
                response = response.Where(o => o.UserId == loggedUserId.Value);
            }

            return await response.ToListAsync();
        }

        public async Task<Order?> GetOrderById(int id, int? loggedUserId, bool isAdmin)
        {
            var response = await _context.Orders
                 .Include(o => o.User)
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .FirstOrDefaultAsync(o => o.Id == id);

            if (response == null)
            {
                throw new KeyNotFoundException($"Order with id - {id} is not available");
            }

            if (!isAdmin && loggedUserId.HasValue && response.UserId != loggedUserId.Value)
            {
                throw new UnauthorizedAccessException("You are not authorized to view this order.");
            }

            return response;
        }

        public async Task<decimal> GetOrderPriceById(int orderId)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new Exception($"Order with id - {order.Id} is not available");
            }

            return order.OrderItems.Sum(x => x.Price * x.Quantity);

        }

        public async Task PayOrder(int orderId, decimal amount, int? loggedUserId, bool isAdmin)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id - {orderId} is not available");
            }
            if (!isAdmin && loggedUserId.HasValue && order.UserId != loggedUserId.Value)
            {
                throw new UnauthorizedAccessException("You cannot pay for an order that does not belong to you.");
            }

            if (amount != order.TotalPrice)
            {
                throw new ArgumentException("Insufficient amount");
            }

            order.IsPaid = true;
          

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with id - {orderId} is not available");
            }

            order.Status = status;

            await _context.SaveChangesAsync();

            //await _emailService.SendEmail(order.User.Email,
            //    "Order status updated",
            //    $"Hello {order.User.Username}, your order #{order.Id} status is now: {status}");
        }
    }
}
