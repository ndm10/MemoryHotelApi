namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RequestRefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
