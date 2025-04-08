using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto
{
    public class ResponseLoginDto : GenericResponseDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
    }
}
