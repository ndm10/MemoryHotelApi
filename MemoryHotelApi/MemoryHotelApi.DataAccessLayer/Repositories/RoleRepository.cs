using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<Role?> FindRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(roleName));
        }
    }
}
