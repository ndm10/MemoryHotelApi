using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
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

        public async Task<int> CountEntitiesAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await _context.Set<T>().CountAsync()
                : await _context.Set<T>().CountAsync(predicate);
        }

        public async Task<List<T>> GenericGetPaginationAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null, string[]? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
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

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(e => e.Order);
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string[]? include = null)
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

        public async Task<T?> GetByIdAsync(Guid id, string[]? includes = null, Expression<Func<T, bool>>? predicate = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (var inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<int> GetMaxOrderAsync()
        {
            if (await _context.Set<T>().AnyAsync(x => !x.IsDeleted))
                return await _context.Set<T>().Where(x => !x.IsDeleted).MaxAsync(x => x.Order);
            else
                return 0;
        }

        public async Task<T?> GetEntityWithConditionAsync(Expression<Func<T, bool>> predicate, string[]? include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = query.Include(inc);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task<T?> GetLastEntityAsync(Expression<Func<T, bool>>? predicate = null, string[]? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
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
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.LastOrDefaultAsync();
        }
    }
}
