using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetAllCities(Expression<Func<City, bool>>? predicate = null)
        {
            var query = _context.Set<City>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(x => x.Tours.OrderBy(t => t.Order))
                .ThenInclude(t => t.SubTours.OrderBy(st => st.Order))
                .ThenInclude(st => st.Images)
                .Include(x => x.Tours)
                .ThenInclude(t => t.Images)
                .Where(x => x.Tours != null && x.Tours.Count() > 0)
                .OrderBy(x => x.Order);

            return await query.OrderBy(x => x.Order).ToListAsync();
        }
    }
}
