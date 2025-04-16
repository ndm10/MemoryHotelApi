using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        public BannerRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public Task<List<Banner>> GetPagination(int pageIndex, int pageSize, string? textSearch, bool? status)
        {
            var query = _context.Banners.AsQueryable();

            if (!string.IsNullOrEmpty(textSearch))
            {
                query = query.Where(b => b.Description != null && b.Description.Contains(textSearch));
            }

            if (status.HasValue)
            {
                query = query.Where(b => b.IsActive == status.Value);
            }

            query = query.Where(b => !b.IsDeleted).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query.ToListAsync();
        }

        public async Task<int> GetMaxOrder()
        {
            var query = _context.Banners.Where(b => !b.IsDeleted)
                .Select(b => b.Order);

            return await query.AnyAsync() ? await query.MaxAsync() : 0;
        }
    }
}
