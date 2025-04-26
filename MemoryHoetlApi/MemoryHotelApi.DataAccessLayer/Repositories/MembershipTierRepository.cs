using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class MembershipTierRepository : GenericRepository<MembershipTier>, IMembershipTierRepository
    {
        public MembershipTierRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }

        public async Task<MembershipTier?> GetMembershipTierByIdAsync(Guid id)
        {
            var query = _context.MembershipTiers
                .Include(x => x.Benefits)
                .ThenInclude(x => x.MembershipTierBenefit)
                .AsQueryable();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MembershipTier>> GetPaginationAsync(int pageIndexValue, int pageSizeValue, Expression<Func<MembershipTier, bool>>? predicate)
        {
            var query = _context.MembershipTiers.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(x => x.Benefits).ThenInclude(x => x.MembershipTierBenefit);

            query = query.Skip((pageIndexValue - 1) * pageSizeValue).Take(pageSizeValue);

            return await query.ToListAsync();
        }
    }
}
