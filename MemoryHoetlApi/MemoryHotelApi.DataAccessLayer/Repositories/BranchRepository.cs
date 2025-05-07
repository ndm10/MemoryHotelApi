using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<List<Branch>> GetAllBranchesAsync()
        {
            var query = _context.Branches
                .Where(b => !b.IsDeleted && b.IsActive)
                .AsNoTracking()
                .AsSplitQuery()
                .Include(b => b.BranchImages)
                .Include(b => b.GeneralConveniences)
                .Include(b => b.HighlightedConveniences)
                .Include(b => b.LocationExplores)
                .Include(b => b.RoomCategories)
                .ThenInclude(rc => rc.Rooms.Where(r => !r.IsDeleted && r.RoomCategory.Branches.Any(br => br.Id == r.BranchId)))
                .ThenInclude(r => r.Conveniences)
                .Include(b => b.RoomCategories)
                .ThenInclude(rct => rct.Rooms)
                .ThenInclude(rm => rm.Images)
                .Include(x => x.BranchImages.OrderBy(bm => bm.Order))
                .ThenInclude(x => x.Image);

            return await query.ToListAsync();
        }

        public async Task<Branch?> GetBranchByIdAsync(Guid id, Expression<Func<Branch, bool>> predicate)
        {
            var branch = await _context.Branches
                .Where(predicate)
                .AsSplitQuery()
                .Include(b => b.BranchImages)
                .Include(b => b.GeneralConveniences)
                .Include(b => b.HighlightedConveniences)
                .Include(b => b.LocationExplores)
                .Include(b => b.RoomCategories)
                .ThenInclude(rc => rc.Rooms.Where(r => !r.IsDeleted && r.RoomCategory.Branches.Any(br => br.Id == r.BranchId)))
                .ThenInclude(r => r.Conveniences)
                .Include(b => b.RoomCategories)
                .ThenInclude(rct => rct.Rooms)
                .ThenInclude(rm => rm.Images)
                .Include(x => x.BranchImages.OrderBy(bm => bm.Order))
                .ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted && b.IsActive);

            return branch;
        }

        public async Task<List<Branch>> GetBranchPaginationAsync(int pageIndexValue, int pageSizeValue, Expression<Func<Branch, bool>>? predicate = null)
        {
            var query = _context.Set<Branch>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.AsNoTracking()
                .AsSplitQuery()
                .Include(b => b.BranchImages)
                .Include(b => b.GeneralConveniences)
                .Include(b => b.HighlightedConveniences)
                .Include(b => b.LocationExplores)
                .Include(b => b.RoomCategories)
                .ThenInclude(rc => rc.Rooms.Where(r => !r.IsDeleted && r.RoomCategory.Branches.Any(br => br.Id == r.BranchId)))
                .ThenInclude(r => r.Conveniences)
                .Include(b => b.RoomCategories)
                .ThenInclude(rct => rct.Rooms)
                .ThenInclude(rm => rm.Images)
                .Include(x => x.BranchImages.OrderBy(bm => bm.Order))
                .ThenInclude(x => x.Image);

            query = query.Skip((pageIndexValue - 1) * pageSizeValue).Take(pageSizeValue);

            return await query.ToListAsync();
        }

        public async Task<Branch?> GetBySlugAsync(string slug)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Slug == slug);
            return branch;
        }
    }
}
