using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<Branch?> GetBySlugAsync(string slug)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
            return branch;
        }
    }
}
