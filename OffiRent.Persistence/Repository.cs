using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.Domain.Repository;

namespace OffiRent.Persistence
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        protected readonly OffiRentDbContext _context;

        public Repository(OffiRentDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            // return _context.Set<T>().AsQueryable();
            return _context.Set<T>();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Single(predicate);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleAsync(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Insert(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public async Task SimpleInsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<T> InsertAsync(T entity)
        {
            var inserted = await _context.Set<T>().AddAsync(entity);
            return inserted.Entity;
        }

        public async Task<T> InsertOrUpdateAsync(T entity)
        {
            try
            {
                return await InsertAsync(entity);
            }
            catch (Exception e)
            {
                // TODO: If exception is "Already Exists"
                return await UpdateAsync(entity);
            }
        }

        public async Task<bool> Contains(T entity)
        {
            // TODO: Watch performance
            return await _context.Set<T>().ContainsAsync(entity);
        }
        
        public Task<T> UpdateAsync(T entity)
        {
            return Task.FromResult<T>(_context.Set<T>().Update(entity).Entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        
        public Task DeleteAsync(T entity)
        {
            return Task.FromResult(_context.Set<T>().Remove(entity));
        }

        public void Delete(int id)
        {
            var t = Get(id);
            _context.Set<T>().Remove(t);
        }

        public async Task DeleteAsync(int id)
        {
            var t = await GetAsync(id);
            _context.Set<T>().Remove(t);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var ts = GetAll().Where(predicate).ToList();
            _context.Set<T>().RemoveRange(ts);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var ts = await GetAll().Where(predicate).ToListAsync();
            _context.Set<T>().RemoveRange(ts);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public Task<int> CountAsync()
        {
            return _context.Set<T>().CountAsync();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().CountAsync(predicate);
        }
    }
}