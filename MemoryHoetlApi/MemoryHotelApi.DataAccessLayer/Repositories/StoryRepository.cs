using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        public StoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Story>> GetAllStories(Expression<Func<Story, bool>>? predicate = null)
        {
            var query = _context.Stories.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.OrderBy(x => x.Order).ToListAsync();
        }
    }
}
