using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class SubFoodCategoryRepository : GenericRepository<SubFoodCategory>, ISubFoodCategoryRepository
    {
        public SubFoodCategoryRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
