using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto
{
    public class RequestSetPasswordDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Otp { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public string? ClientIp { get; set; }
    }
}
