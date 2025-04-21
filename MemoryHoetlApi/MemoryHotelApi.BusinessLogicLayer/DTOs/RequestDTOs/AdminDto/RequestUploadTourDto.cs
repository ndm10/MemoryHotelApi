using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadTourDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề phụ")]
        public string SubTitle { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập thành phố của tour")]
        public Guid CityId { get; set; }

        public int? Order { get; set; }

        [Required(ErrorMessage = "Vui chọn ảnh cho tour")]
        public required List<string> ImageUrls { get; set; }
    }
}
