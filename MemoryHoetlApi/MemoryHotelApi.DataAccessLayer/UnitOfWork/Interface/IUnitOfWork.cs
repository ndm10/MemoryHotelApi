using Microsoft.EntityFrameworkCore.Storage;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using MemoryHotelApi.DataAccessLayer.Repositories;

namespace MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? UserRepository { get; }
        IImageRepository? ImageRepository { get; }
        IRoleRepository? RoleRepository { get; }
        Task<int> SaveChangeAsync();
        IDbContextTransaction BeginTransaction();
    }
}
