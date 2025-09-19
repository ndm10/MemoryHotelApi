using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadFoodDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đồ ăn")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập giá đồ ăn")]
        [NumberGreaterThan(0, ErrorMessage = "Vui lòng nhập thời gian chờ món (lớn hơn 0 phút)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hình ảnh đồ ăn")]
        public string Image { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Vui lòng nhập thời gian chờ món (lớn hơn 0 phút)")]
        public int WaitingTimeInMinute { get; set; }

        public bool IsBestSeller { get; set; } = false;
        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục đồ ăn")]
        public Guid SubFoodCategoryId { get; set; }
        
        public int? Order { get; set; }
    }
}
