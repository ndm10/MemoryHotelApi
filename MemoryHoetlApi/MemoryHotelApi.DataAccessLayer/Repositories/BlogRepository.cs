using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public Task<Blog?> GetBlogByIdAsync(Guid id, Expression<Func<Blog, bool>>? predicate = null)
        {
            var query = _context.Set<Blog>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(x => x.Author)
                         .Include(x => x.BlogHashtags)
                         .ThenInclude(x => x.Hashtag);

            return query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Blog>> GetBlogsPaginationAsync(int pageIndex, int pageSize, Expression<Func<Blog, bool>>? predicate = null)
        {
            var query = _context.Set<Blog>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(x => x.Author).Include(x => x.BlogHashtags).ThenInclude(x => x.Hashtag);

            query = query.OrderBy(x => x.Order).ThenBy(x => x.Title).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
