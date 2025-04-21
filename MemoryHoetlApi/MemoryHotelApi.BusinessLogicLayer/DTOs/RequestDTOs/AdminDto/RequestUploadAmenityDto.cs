using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadAmenityDto
    {
        [Required(ErrorMessage = "Vui lòng điền icon của dịch vụ")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập key")]
        public string Key { get; set; } = null!;

        public string? Description { get; set; }
        
        public int? Order { get; set; }
    }
}
