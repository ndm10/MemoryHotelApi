using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class ZaloOaAuthenticationTokenRepository : GenericRepository<ZaloOaAuthenticationToken>, IZaloOaAuthenticationTokenRepository
    {
        public ZaloOaAuthenticationTokenRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
