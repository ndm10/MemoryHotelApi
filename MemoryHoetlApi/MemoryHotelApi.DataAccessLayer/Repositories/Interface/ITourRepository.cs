using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface ITourRepository : IGenericRepository<Tour>
    {
        Task<Tour?> GetTourByIdAsync(Guid id);
        public Task<List<Tour>> GetTourPaginationAsync(int pageIndex, int pageSize, Expression<Func<Tour, bool>>? predicate = null);
    }
}
