using E_Commerce.Data;
using E_Commerce.Data.Data;
using E_Commerce.Repository.Interfaces;
using E_Commerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly ECommerceDbContext _context;
        public UserService(IBaseRepository<User> repo,
            ECommerceDbContext context) : base(repo)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
               .Include(u => u.Orders)
               .ThenInclude(ui => ui.OrderItems)
               .ThenInclude(p=>p.Product)
               .ToListAsync();
        }
    }
}
