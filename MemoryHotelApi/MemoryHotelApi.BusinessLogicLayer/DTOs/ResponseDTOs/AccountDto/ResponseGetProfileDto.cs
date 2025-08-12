using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto
{
    public class ResponseGetProfileDto : BaseResponseDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Nationality { get; set; }
        public MembershipTierDtoCommon? MembershipTier { get; set; }
        public MembershipTierDtoCommon? NextMembershipTier { get; set; }
    }
}
