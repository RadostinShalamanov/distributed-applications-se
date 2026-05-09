using E_Commerce.Data;
using E_Commerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ECommerceDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ECommerceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {

            await Task.FromResult(_dbSet.Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task Edit(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await GetAll(Array.Empty<Expression<Func<T, object>>>());
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetById(object[] id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
