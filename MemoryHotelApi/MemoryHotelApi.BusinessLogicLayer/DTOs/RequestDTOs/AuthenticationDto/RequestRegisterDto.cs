using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RequestRegisterDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        [JsonIgnore]
        public string? ClientIp { get; set; }
    }
}
