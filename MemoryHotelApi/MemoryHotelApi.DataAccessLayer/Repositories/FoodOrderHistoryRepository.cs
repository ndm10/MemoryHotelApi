using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class FoodOrderHistoryRepository : GenericRepository<FoodOrderHistory>, IFoodOrderHistoryRepository
    {
        public FoodOrderHistoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<FoodOrderHistory?> GetLastOrderAsync()
        {
            return await _context.FoodOrderHistories
                .Where(order => !order.IsDeleted)
                .OrderByDescending(order => order.CreatedDate)
                .FirstOrDefaultAsync();
        }
    }
}
