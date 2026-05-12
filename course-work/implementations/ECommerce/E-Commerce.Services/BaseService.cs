using E_Commerce.Repository.Interfaces;
using E_Commerce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repo;

        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task Add(T entity)
        {
            await _repo.Create(entity);
        }

        public async Task Delete(int id)
        {
            var toDelete = await _repo.GetById(id);

            await _repo.Delete(toDelete);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return await _repo.GetAll(includes);
        }

        public async Task<T> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Update(T entity)
        {
            await _repo.Edit(entity);
        }
    }
}
