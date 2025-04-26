using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetMembershipTierBenefitsDto : GenericResponsePagination<MembershipTierBenefitDto>
    {
    }

    public class MembershipTierBenefitDto
    {
        public Guid Id { get; set; }
        public string Benefit { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
