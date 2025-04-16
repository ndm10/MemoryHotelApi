using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IGenericRepository<T> where T : GenericEntity
    {
        Task<List<T>> GenericGetPaginationAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<T?> GetByIdIncludeAsync(Guid id, string[] includes);
    }
}
