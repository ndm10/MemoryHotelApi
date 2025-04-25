namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class MembershipTier : BaseEntity
    {
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<MembershipTierMembershipTierBenefit> Benefits = new List<MembershipTierMembershipTierBenefit>();
    }
}
