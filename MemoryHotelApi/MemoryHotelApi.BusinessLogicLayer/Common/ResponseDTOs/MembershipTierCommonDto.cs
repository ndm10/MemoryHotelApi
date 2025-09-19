using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class MembershipTierCommonDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<MembershipTierMembershipTierBenefitCommonDto> Benefits { get; set; } = new List<MembershipTierMembershipTierBenefitCommonDto>();
    }
}
