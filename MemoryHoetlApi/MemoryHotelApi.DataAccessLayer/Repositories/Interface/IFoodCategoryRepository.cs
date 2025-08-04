using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IFoodCategoryRepository : IGenericRepository<FoodCategory>
    {
        Task<bool> IsIncludeSubFoodCategoryAsync(Guid foodCategoryId, Guid subFoodCategoryId);
    }
}
