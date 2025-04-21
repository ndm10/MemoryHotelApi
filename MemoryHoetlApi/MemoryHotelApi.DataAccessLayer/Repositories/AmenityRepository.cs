using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
