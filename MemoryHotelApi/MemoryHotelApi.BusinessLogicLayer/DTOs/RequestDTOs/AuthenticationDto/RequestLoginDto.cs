namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RequestLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
