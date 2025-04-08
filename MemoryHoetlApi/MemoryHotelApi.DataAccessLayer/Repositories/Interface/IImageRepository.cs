using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task AddImageAsync(Image image);
    }
}
