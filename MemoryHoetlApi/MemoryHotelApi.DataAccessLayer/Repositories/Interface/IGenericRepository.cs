using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GenericGetPaginationAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null, string[]? include = null);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task<T?> GetByIdAsync(Guid id, string[]? includes = null, Expression<Func<T, bool>>? predicate = null);
        public Task<T?> GetByIdIncludeAsync(Guid id, string[] includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string[]? include = null);
        Task<int> GetMaxOrder();
        Task<T?> GetWithCondition(Expression<Func<T, bool>> predicate, string[]? include = null);
        Task<int> CountEntities(Expression<Func<T, bool>>? predicate = null);
    }
}
