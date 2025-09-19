using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class CarBookingHistoryDetailRepository : GenericRepository<CarBookingHistoryDetail>, ICarBookingHistoryDetailRepository
    {
        public CarBookingHistoryDetailRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
