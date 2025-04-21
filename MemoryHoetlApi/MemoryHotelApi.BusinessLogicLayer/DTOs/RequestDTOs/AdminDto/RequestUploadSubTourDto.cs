using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadSubTourDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập thời gian khởi hành")]
        public string DepartureTime { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập thời lượng của tour")]
        [Range(0, int.MaxValue, ErrorMessage = "Thời lượng hợp lệ")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập phương tiện di chuyển")]
        public string Transportation { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập lịch trình")]
        public string TravelSchedule { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Vui lòng nhập giá tiền lớn hơn 0")]
        public decimal Price { get; set; }

        [NotDefault(ErrorMessage = "Mã tour chính không hợp lệ")]
        public Guid TourId { get; set; }

        [Required(ErrorMessage = "Vui chọn ảnh cho tour")]
        public required List<string> ImageUrls { get; set; }
        public int? Order { get; set; }
    }
}
