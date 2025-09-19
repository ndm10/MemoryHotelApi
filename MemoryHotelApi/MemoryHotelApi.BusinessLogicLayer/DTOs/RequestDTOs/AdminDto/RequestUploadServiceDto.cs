using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadServiceDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập giá dịch vụ")]
        [NumberGreaterThan(0, ErrorMessage = "Vui lòng nhập giá dịch vụ (lớn hơn 0 phút)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hình ảnh dịch vụ")]
        public string Image { get; set; } = null!;
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục dịch vụ")]
        public Guid ServiceCategoryId { get; set; }

        public int? Order { get; set; }
    }
}
