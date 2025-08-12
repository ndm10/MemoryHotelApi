using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class MembershipTierDtoCommon
    {
        public Guid Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<MembershipTierMembershipTierBenefitDtoCommon> Benefits { get; set; } = new List<MembershipTierMembershipTierBenefitDtoCommon>();
    }
}
