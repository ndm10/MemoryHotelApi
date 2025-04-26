using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class MembershipTierBenefitRepository : GenericRepository<MembershipTierBenefit>, IMembershipTierBenefitRepository
    {
        public MembershipTierBenefitRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
