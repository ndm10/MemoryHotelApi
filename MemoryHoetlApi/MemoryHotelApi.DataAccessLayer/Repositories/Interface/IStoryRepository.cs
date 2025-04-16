using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        Task<int> GetMaxOrder();
    }
}
