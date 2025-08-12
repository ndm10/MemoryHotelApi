using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class ConvenienceRepository : GenericRepository<Convenience>, IConvenienceRepository
    {
        public ConvenienceRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
