using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadBannerDto
    {
        [Required(ErrorMessage = "Vui lòng điền đầy đủ thông tin!")]
        public required List<UploadBannerDto> Banners { get; set; }
    }

    public class UploadBannerDto
    {
        [Required(ErrorMessage = "Vui lòng điền link ảnh!")]
        public required string ImageUrl { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
