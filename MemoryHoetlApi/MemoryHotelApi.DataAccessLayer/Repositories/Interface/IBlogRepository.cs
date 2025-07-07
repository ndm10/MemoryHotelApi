using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<Blog?> GetBlogByIdAsync(Guid id, Expression<Func<Blog, bool>>? predicate = null);
        Task<List<Blog>> GetBlogsPaginationAsync(int pageIndex, int pageSize, Expression<Func<Blog, bool>>? predicate = null);
    }
}
