using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class FoodOrderHistoryRepository : GenericRepository<FoodOrderHistory>, IFoodOrderHistoryRepository
    {
        public FoodOrderHistoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
