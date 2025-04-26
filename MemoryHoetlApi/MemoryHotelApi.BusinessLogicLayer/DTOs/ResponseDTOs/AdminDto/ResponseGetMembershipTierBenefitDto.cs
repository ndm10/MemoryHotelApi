using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetMembershipTierBenefitDto : BaseResponseDto
    {
        public MembershipTierBenefitDto? Data { get; set; }
    }
}
