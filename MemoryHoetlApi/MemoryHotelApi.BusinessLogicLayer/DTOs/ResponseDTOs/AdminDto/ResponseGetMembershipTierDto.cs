using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetMembershipTierDto : BaseResponseDto
    {
        public MembershipTierDto? Data { get; set; }
    }
}
