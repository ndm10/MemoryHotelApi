using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IEnumerable<City>> GetAllCities(Expression<Func<City, bool>>? predicate = null);
    }
}
