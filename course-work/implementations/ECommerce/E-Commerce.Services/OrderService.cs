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

namespace E_Commerce.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly ECommerceDbContext _context;


        public OrderService(ECommerceDbContext context,
            IBaseRepository<Order> repository) : base(repository)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            var user = await _context.Users.FindAsync(order.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            decimal totalPrice = 0;
            foreach (var item in order.OrderItems)
            {


                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with id - {item.ProductId} not found!");
                }

                item.Price = product.Price;
                totalPrice += product.Price * item.Quantity;
            }

            order.TotalPrice = totalPrice;
            _context.Orders.Add(order);

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
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

        public async Task PayOrder(int orderId, decimal amount)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id - {order.Id} is not available");
            }

            if (amount < order.TotalPrice || amount > order.TotalPrice)
            {
                throw new Exception("Insufficient amount");
            }

            order.IsPaid = true;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id - {order.Id} is not available");
            }

            order.Status = status;

            await _context.SaveChangesAsync();
        }
    }
}
