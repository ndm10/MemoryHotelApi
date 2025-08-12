using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadRoomDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mô tả phòng")]
        public string Description { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Diện tích phòng phải lớn hơn 0")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập loại giường")]
        public string BedType { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Số lượng người tối đa phải lớn hơn 0")]
        public int Capacity { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Giá phòng phải lớn hơn 0")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục phòng")]
        public Guid RoomCategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn chi nhánh")]
        public Guid BranchId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất 1 tiện nghi")]
        public List<Guid> ConvenienceIds { get; set; } = new List<Guid>();

        [Required(ErrorMessage = "Vui lòng chọn ít nhất 1 hình ảnh")]
        public List<string> ImageUrls { get; set; } = new List<string>();

    }
}
