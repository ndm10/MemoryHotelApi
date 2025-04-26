namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class MembershipTierMembershipTierBenefit
    {
        public Guid MembershipTierId { get; set; }
        public MembershipTier MembershipTier { get; set; } = new MembershipTier();

        public Guid MembershipTierBenefitId { get; set; }
        public MembershipTierBenefit MembershipTierBenefit { get; set; } = new MembershipTierBenefit();

        public string? Value { get; set; }
    }
}
