using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IBannerRepository : IGenericRepository<Banner>
    {
        Task<int> GetMaxOrder();
    }
}
