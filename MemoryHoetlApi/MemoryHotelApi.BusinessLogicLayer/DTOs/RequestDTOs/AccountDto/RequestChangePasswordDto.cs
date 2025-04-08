using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto
{
    public class RequestChangePasswordDto
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [Required]
        public string OldPassword { get; set; } = null!;

        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
