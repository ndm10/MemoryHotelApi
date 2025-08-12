using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto
{
    public class RequestUpdateProfileDto
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Vui lòng điền họ và tên!")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền số điện thoại!")]
        public string Phone { get; set; } = null!;

        public string? Nationality { get; set; }
    }
}
