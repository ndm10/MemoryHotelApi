using Microsoft.EntityFrameworkCore.Storage;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using MemoryHotelApi.DataAccessLayer.Repositories;

namespace MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? UserRepository { get; }
        IImageRepository? ImageRepository { get; }
        IStoryRepository? IStoryRepository { get; }
        IRoleRepository? RoleRepository { get; }
        IBannerRepository? BannerRepository { get; }
        ICityRepository? CityRepository { get; }
        ITourRepository? TourRepository { get; }
        ISubTourRepository? SubTourRepository { get; }
        IBranchRepository? BranchRepository { get; }
        IConvenienceRepository? ConvenienceRepository { get; }
        IRoomCategoryRepository? RoomCategoryRepository { get; }
        IMembershipTierRepository? MembershipTierRepository { get; }
        IMembershipTierBenefitRepository? MembershipTierBenefitRepository { get; }
        IRoomRepository? RoomRepository { get; }
        IBlogRepository? BlogRepository { get; }
        IHashtagRepository? HashtagRepository { get; }
        IFoodCategoryRepository? FoodCategoryRepository { get; }
        ISubFoodCategoryRepository? SubFoodCategoryRepository { get; }
        IFoodRepository? FoodRepository { get; }
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
    }
}
