using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {
        Task<List<Branch>> GetAllBranchesAsync();
        Task<Branch?> GetBranchByIdAsync(Guid id, Expression<Func<Branch, bool>> predicate);
        Task<List<Branch>> GetBranchPaginationAsync(int pageIndexValue, int pageSizeValue, Expression<Func<Branch, bool>>? predicate = null);
        Task<Branch?> GetBySlugAsync(string slug);
    }
}
