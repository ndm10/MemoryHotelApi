using Microsoft.EntityFrameworkCore.Storage;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? UserRepository { get; }
        Task<int> SaveChangeAsync();
        IDbContextTransaction BeginTransaction();
    }
}
