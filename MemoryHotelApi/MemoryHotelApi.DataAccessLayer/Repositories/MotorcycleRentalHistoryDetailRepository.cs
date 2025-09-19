using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class MotorcycleRentalHistoryDetailRepository : GenericRepository<MotorcycleRentalHistoryDetail>, IMotorcycleRentalHistoryDetailRepository
    {
        public MotorcycleRentalHistoryDetailRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
