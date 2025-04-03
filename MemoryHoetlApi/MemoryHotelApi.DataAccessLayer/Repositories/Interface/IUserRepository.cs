using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> FindUserByRefreshToken(Guid userId, string refreshToken);
        Task<User?> FindUserByEmail(string email);
    }
}
