using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task AddImageAsync(Image image)
        {
            var entry = await _context.Images.AddAsync(image);
        }

        public async Task<List<Image>> GetImagesWithCondition(Expression<Func<Image, bool>> predicate)
        {
            var query = _context.Set<Image>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
    }
}
