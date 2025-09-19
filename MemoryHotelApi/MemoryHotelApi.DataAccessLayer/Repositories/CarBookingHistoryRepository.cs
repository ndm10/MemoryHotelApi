using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    internal class CarBookingHistoryRepository : GenericRepository<CarBookingHistory>, ICarBookingHistoryRepository
    {
        public CarBookingHistoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
