using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public Task<Role?> FindRoleByNameAsync(string roleName);
    }
}
