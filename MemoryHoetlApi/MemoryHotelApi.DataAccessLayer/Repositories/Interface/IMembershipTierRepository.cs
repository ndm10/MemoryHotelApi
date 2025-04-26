using MemoryHotelApi.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace MemoryHotelApi.DataAccessLayer.Repositories.Interface
{
    public interface IMembershipTierRepository : IGenericRepository<MembershipTier>
    {
        Task<MembershipTier?> GetMembershipTierByIdAsync(Guid id);
        Task<IEnumerable<MembershipTier>> GetPaginationAsync(int pageIndexValue, int pageSizeValue, Expression<Func<MembershipTier, bool>>? predicate);
    }
}
