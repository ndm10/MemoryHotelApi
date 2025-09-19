using Microsoft.EntityFrameworkCore.Storage;
using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Repositories;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MemoryHotelApiDbContext _context;
        private IUserRepository _userRepository = null!;
        private IImageRepository _imageRepository = null!;
        private IRoleRepository _roleRepository = null!;
        private IBannerRepository _bannerRepository = null!;
        private IStoryRepository _storyRepository = null!;
        private ICityRepository _cityRepository = null!;
        private ITourRepository _tourRepository = null!;
        private ISubTourRepository _subTourRepository = null!;
        private IBranchRepository _branchRepository = null!;
        private IConvenienceRepository _convenienceRepository = null!;
        private IRoomCategoryRepository _roomCategoryRepository = null!;
        private IMembershipTierRepository _membershipTierRepository = null!;
        private IMembershipTierBenefitRepository _membershipTierBenefitRepository = null!;
        private IRoomRepository _roomRepository = null!;
        private IBlogRepository? _blogRepository = null!;
        private IHashtagRepository? _hashtagRepository = null!;
        private IFoodCategoryRepository? _foodCategoryRepository = null!;
        private ISubFoodCategoryRepository? _subFoodCategoryRepository = null!;
        private IFoodRepository? _foodRepository = null!;
        private IFoodOrderHistoryRepository _foodOrderHistoryRepository = null!;
        private IGroupChatZaloRepository? _groupChatZaloRepository = null!;
        private IZaloOaAuthenticationTokenRepository _zaloOaAuthenticationTokenRepository = null!;
        private IServiceCategoryRepository? _serviceCategoryRepository = null!;
        private IMotorcycleRentalHistoryRepository? _motorcycleRentalHistoryRepository = null!;
        private IMotorcycleRentalHistoryDetailRepository? _motorcycleRentalHistoryDetailRepository = null!;
        private ICarBookingHistoryRepository? _carBookingHistoryRepository = null!;
        private ICarBookingHistoryDetailRepository? _carBookingHistoryDetailRepository = null!;
        private bool _disposed = false;

        public UnitOfWork(MemoryHotelApiDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public IImageRepository? ImageRepository
        {
            get
            {
                return _imageRepository ??= new ImageRepository(_context);
            }
        }

        public IRoleRepository? RoleRepository
        {
            get
            {
                return _roleRepository ??= new RoleRepository(_context);
            }
        }

        public IBannerRepository? BannerRepository
        {
            get
            {
                return _bannerRepository ??= new BannerRepository(_context);
            }
        }

        public IStoryRepository? IStoryRepository
        {
            get
            {
                return _storyRepository ??= new StoryRepository(_context);
            }
        }

        public ICityRepository? CityRepository
        {
            get
            {
                return _cityRepository ??= new CityRepository(_context);
            }
        }

        public ITourRepository? TourRepository
        {
            get
            {
                return _tourRepository ??= new TourRepository(_context);
            }
        }

        public ISubTourRepository? SubTourRepository
        {
            get
            {
                return _subTourRepository ??= new SubTourRepository(_context);
            }
        }

        public IBranchRepository? BranchRepository
        {
            get
            {
                return _branchRepository ??= new BranchRepository(_context);
            }
        }

        public IConvenienceRepository? ConvenienceRepository
        {
            get
            {
                return _convenienceRepository ??= new ConvenienceRepository(_context);
            }
        }

        public IRoomCategoryRepository? RoomCategoryRepository
        {
            get
            {
                return _roomCategoryRepository ??= new RoomCategoryRepository(_context);
            }
        }

        public IMembershipTierRepository? MembershipTierRepository
        {
            get
            {
                return _membershipTierRepository ??= new MembershipTierRepository(_context);
            }
        }

        public IMembershipTierBenefitRepository? MembershipTierBenefitRepository
        {
            get
            {
                return _membershipTierBenefitRepository ??= new MembershipTierBenefitRepository(_context);
            }
        }

        public IRoomRepository? RoomRepository
        {
            get
            {
                return _roomRepository ??= new RoomRepository(_context);
            }
        }

        public IBlogRepository? BlogRepository
        {
            get
            {
                return _blogRepository ??= new BlogRepository(_context);
            }
        }

        public IHashtagRepository? HashtagRepository
        {
            get
            {
                return _hashtagRepository ??= new HashtagRepository(_context);
            }
        }

        public IFoodCategoryRepository? FoodCategoryRepository
        {
            get
            {
                return _foodCategoryRepository ??= new FoodCategoryRepository(_context);
            }
        }

        public ISubFoodCategoryRepository? SubFoodCategoryRepository
        {
            get
            {
                return _subFoodCategoryRepository ??= new SubFoodCategoryRepository(_context);
            }
        }

        public IFoodRepository? FoodRepository
        {
            get
            {
                return _foodRepository ??= new FoodRepository(_context);
            }
        }

        public IFoodOrderHistoryRepository FoodOrderHistoryRepository
        {
            get
            {
                return _foodOrderHistoryRepository ??= new FoodOrderHistoryRepository(_context);
            }
        }

        public IGroupChatZaloRepository GroupChatZaloRepository
        {
            get
            {
                return _groupChatZaloRepository ??= new GroupChatZaloRepository(_context);
            }
        }

        public IZaloOaAuthenticationTokenRepository ZaloOaAuthenticationTokenRepository
        {
            get
            {
                return _zaloOaAuthenticationTokenRepository ??= new ZaloOaAuthenticationTokenRepository(_context);
            }
        }

        public IServiceCategoryRepository? ServiceCategoryRepository
        {
            get
            {
                return _serviceCategoryRepository ??= new ServiceCategoryRepository(_context);
            }
        }

        public IMotorcycleRentalHistoryRepository? MotorcycleRentalHistoryRepository
        {
            get
            {
                return _motorcycleRentalHistoryRepository ??= new MotorcycleRentalHistoryRepository(_context);
            }
        }

        public IMotorcycleRentalHistoryDetailRepository? MotorcycleRentalHistoryDetailRepository
        {
            get
            {
                return _motorcycleRentalHistoryDetailRepository ??= new MotorcycleRentalHistoryDetailRepository(_context);
            }
        }

        public ICarBookingHistoryRepository? CarBookingHistoryRepository
        {
            get
            {
                return _carBookingHistoryRepository ??= new CarBookingHistoryRepository(_context);
            }
        }

        public ICarBookingHistoryDetailRepository? CarBookingHistoryDetailRepository
        {
            get
            {
                return _carBookingHistoryDetailRepository ??= new CarBookingHistoryDetailRepository(_context);
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
