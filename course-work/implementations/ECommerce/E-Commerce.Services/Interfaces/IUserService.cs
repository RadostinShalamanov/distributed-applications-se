using E_Commerce.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByEmail(string email);

        Task<IEnumerable<User>> GetUsers();
    }
}
