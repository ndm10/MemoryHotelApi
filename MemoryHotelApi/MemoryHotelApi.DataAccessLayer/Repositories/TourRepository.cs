using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class TourRepository : GenericRepository<Tour>, ITourRepository
    {
        public TourRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<Tour?> GetTourByIdAsync(Guid id)
        {
            var query = _context.Set<Tour>().AsQueryable();

            query = query.Include(x => x.Images).Include(x => x.City).Include(x => x.SubTours).ThenInclude(x => x.Images);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Tour>> GetTourPaginationAsync(int pageIndex, int pageSize, Expression<Func<Tour, bool>>? predicate = null)
        {
            var query = _context.Set<Tour>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            query = query.Include(x => x.Images).Include(x => x.City).Include(x => x.SubTours).ThenInclude(x => x.Images);

            return await query.ToListAsync();
        }
    }
}
