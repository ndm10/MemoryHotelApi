using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity
    {
        protected readonly MemoryHotelApiDbContext _context;

        public GenericRepository(MemoryHotelApiDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<List<T>> GenericGetPaginationAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null, string[]? include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if(include != null)
            {
                foreach (var inc in include)
                {
                    query = query.Include(inc);
                }
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string[]? include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = query.Include(inc);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdIncludeAsync(Guid id, string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
