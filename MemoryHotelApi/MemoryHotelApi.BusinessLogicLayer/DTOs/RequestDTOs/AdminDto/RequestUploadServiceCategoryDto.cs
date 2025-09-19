using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadServiceCategoryDto
    {
        [Required(ErrorMessage = "Vui lòng điền tên phân loại Category")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng điền icon cho phân loại Category")]
        public string Icon { get; set; } = null!;

        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
