using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadBranchDto
    {
        [Required(ErrorMessage = "Vui lòng điền tên chi nhánh")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền địa chỉ chi nhánh")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền điểm nổi bật của chi nhánh")]
        public string LocationHighlights { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền đối tượng phù hợp của chi nhánh")]
        public string SuitableFor { get; set; } = null!;

        [NumberGreaterThan(0, ErrorMessage = "Giá phòng phải lớn hơn 0 đồng")]
        [Required(ErrorMessage = "Vui lòng điền giá phòng của chi nhánh")]
        public decimal PricePerNight { get; set; }
        
        public string? Description { get; set; } = null!;

        public int? Order { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();

        public List<Guid> AmenityIDs { get; set; } = new List<Guid>();
    }
}
