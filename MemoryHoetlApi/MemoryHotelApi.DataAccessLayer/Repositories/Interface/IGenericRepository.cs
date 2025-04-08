using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IGenericRepository<T> where T : GenericEntity
    {
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task<T?> GetByIdAsync(Guid id);
    }
}
