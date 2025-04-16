using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadStoryDto
    {
        [Required(ErrorMessage = "Vui lòng điền link ảnh!")]
        public required string ImageUrl { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
