using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IFoodOrderHistoryRepository : IGenericRepository<FoodOrderHistory>
    {
        Task<FoodOrderHistory?> GetLastOrderByBranchIdAsync(Guid branchId);
    }
}
