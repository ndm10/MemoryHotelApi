namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class MembershipTierBenefitCommonDto
    {
        public Guid Id { get; set; }
        public string Benefit { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}
