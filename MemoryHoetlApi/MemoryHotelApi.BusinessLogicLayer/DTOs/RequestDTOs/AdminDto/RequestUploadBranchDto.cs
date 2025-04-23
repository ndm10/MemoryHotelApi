using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadBranchDto
    {
        [Required(ErrorMessage = "Vui lòng điền tên khách sạn")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền địa chỉ khách sạn")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền điểm nổi bật của khách sạn")]
        public List<string> LocationHighlights { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền đối tượng phù hợp của khách sạn")]
        public List<string> SuitableFor { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Giá phòng phải lớn hơn 0 đồng")]
        [Required(ErrorMessage = "Vui lòng điền giá phòng của khách sạn")]
        public decimal PricePerNight { get; set; }

        public string? Description { get; set; } = null!;

        public int? Order { get; set; }

        public string? Slug { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();

        public List<Guid> GeneralConvenienceIDs { get; set; } = new List<Guid>();

        public List<Guid> HighlightedConvenienceIDs { get; set; } = new List<Guid>();

        public List<UploadLocationExploreDto> LocationExplores { get; set; } = new List<UploadLocationExploreDto>();
    }

    public class UploadLocationExploreDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên địa điểm")]
        public string Name { get; set; } = null!;
        
        [Required(ErrorMessage = "Vui lòng nhập khoảng cách")]
        public string Distance { get; set; } = null!;
    }
}
