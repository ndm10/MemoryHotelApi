using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateBranchDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<string>? LocationHighlights { get; set; }
        public List<string>? SuitableFor { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Giá phòng phải lớn hơn 0 đồng")]
        public decimal? PricePerNight { get; set; }

        public string? BranchLocation { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
        public string? HotelCode { get; set; }
        public bool? IsActive { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<Guid>? GeneralConvenienceIDs { get; set; }
        public List<Guid>? HighlightedConvenienceIDs { get; set; }
        public List<UploadLocationExploreDto>? LocationExplores { get; set; }
        public List<Guid>? RoomCategoryIDs { get; set; }
        public string? HotLine { get; set; }
    }
}
