using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateRoomDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Diện tích phòng phải lớn hơn 0")]
        public double? Area { get; set; }
        public string? BedType { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Số lượng người tối đa phải lớn hơn 0")]
        public int? Capacity { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Giá phòng phải lớn hơn 0")]
        public decimal? PricePerNight { get; set; }
        public Guid? RoomCategoryId { get; set; }
        public Guid? BranchId { get; set; }
        public List<Guid>? ConvenienceIds { get; set; }
        public List<string>? ImageUrls { get; set; }
        public bool? IsActive { get; set; }
    }
}
