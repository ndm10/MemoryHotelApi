using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadMembershipTierBenefitDto
    {
        [Required(ErrorMessage = "Vui lòng nhập quyền lợi")]
        public string Benefit { get; set; } = null!;

        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
