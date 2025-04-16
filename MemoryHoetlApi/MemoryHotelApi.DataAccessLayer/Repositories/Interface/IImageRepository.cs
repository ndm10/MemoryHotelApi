using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task AddImageAsync(Image image);
        Task<List<Image>> GetImagesWithCondition(Expression<Func<Image, bool>> predicate);
    }
}
