using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto
{
    public class ResponseLoginDto : BaseResponseDto
    {
        public string? Token { get; set; }
        public DateTime? ExpiredTimeToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiredTimeRefreshToken { get; set; }
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public Guid? BranchId { get; set; }
    }
}
