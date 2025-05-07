using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<User?> FindUserByRefreshToken(Guid userId,string refreshToken)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => (u.Id == userId) && (u.RefreshToken == refreshToken));
            
            return user;
        }

        public async Task<User?> FindUserByEmail(string email)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<User?> GetUserProfileAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.MembershipTier)
                .ThenInclude(m => m.Benefits)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        }
    }
}
