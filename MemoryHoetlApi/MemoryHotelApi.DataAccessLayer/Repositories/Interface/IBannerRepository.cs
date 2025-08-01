using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IBannerRepository : IGenericRepository<Banner>
    {
        Task<List<Banner>> GetPagination(int pageIndex, int pageSize, string? textSearch, bool? status);
    }
}
