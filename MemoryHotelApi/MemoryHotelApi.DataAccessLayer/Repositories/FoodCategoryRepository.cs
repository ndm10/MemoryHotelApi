using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class FoodCategoryRepository : GenericRepository<FoodCategory>, IFoodCategoryRepository
    {
        public FoodCategoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<bool> IsIncludeSubFoodCategoryAsync(Guid foodCategoryId, Guid subFoodCategoryId)
        {
            return await _context.FoodCategories.Where(fc => fc.Id == foodCategoryId && fc.SubFoodCategories.Any(sfc => sfc.Id == subFoodCategoryId))
                .AnyAsync();
        }
    }
}
