using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadSubFoodCategoryDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên cho phân loại món")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn danh mục món ăn")]
        public Guid FoodCategoryId { get; set; }

        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
