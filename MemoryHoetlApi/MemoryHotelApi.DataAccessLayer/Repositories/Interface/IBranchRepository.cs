using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {
        Task<Branch?> GetBySlugAsync(string slug);
    }
}
