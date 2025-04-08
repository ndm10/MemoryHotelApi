using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RegisterVerifyOtpRequestDto
    {
        public string? Otp { get; set; }
        public string? Email { get; set; } = null!;

        [JsonIgnore]
        public string? ClientIp { get; set; }
    }
}
