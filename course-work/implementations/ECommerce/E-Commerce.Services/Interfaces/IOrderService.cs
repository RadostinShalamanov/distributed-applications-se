using E_Commerce.Data.Data;
using E_Commerce.Data.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<Order> CreateOrder(Order order);

        Task<IEnumerable<Order>> GetAllOrders(int? loggedUserId, bool isAdmin);

        Task<Order> GetOrderById(int id, int? loggedUserId, bool isAdmin);

        Task<decimal> GetOrderPriceById(int orderId);

        Task UpdateStatus(int orderId, OrderStatus status);

        Task PayOrder(int orderId, decimal amount, int? loggedUserId, bool isAdmin);
    }
}
