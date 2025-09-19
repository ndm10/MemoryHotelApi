using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class MotorcycleRentalHistoryRepository : GenericRepository<MotorcycleRentalHistory>, IMotorcycleRentalHistoryRepository
    {
        public MotorcycleRentalHistoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
