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
