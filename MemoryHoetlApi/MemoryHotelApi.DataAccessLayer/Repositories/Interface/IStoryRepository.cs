using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        Task<IEnumerable<Story>> GetAllStories(Expression<Func<Story, bool>>? predicate = null);
        Task<int> GetMaxOrder();
    }
}
