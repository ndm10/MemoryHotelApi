using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

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
    }
}
