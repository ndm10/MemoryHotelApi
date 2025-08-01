using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class FoodCategoryRepository : GenericRepository<FoodCategory>, IFoodCategoryRepository
    {
        public FoodCategoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
