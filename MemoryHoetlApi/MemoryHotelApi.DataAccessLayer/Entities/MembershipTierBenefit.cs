namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class MembershipTierBenefit : BaseEntity
    {
        public string Benefit { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<MembershipTierMembershipTierBenefit> Tiers = new List<MembershipTierMembershipTierBenefit>();
    }
}
