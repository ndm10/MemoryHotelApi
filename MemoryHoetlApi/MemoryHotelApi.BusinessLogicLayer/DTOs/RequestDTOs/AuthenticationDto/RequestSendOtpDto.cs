using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RequestSendOtpDto
    {
        public string? Email { get; set; }

        [JsonIgnore]
        public string? ClientIp { get; set; }
    }
}
