using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        public Task<T> GetById(int id);
        public Task<T> GetById(object[] id);

        public Task Create(T author);

        public Task Edit(T author);

        public Task Delete(T author);
    }
}
