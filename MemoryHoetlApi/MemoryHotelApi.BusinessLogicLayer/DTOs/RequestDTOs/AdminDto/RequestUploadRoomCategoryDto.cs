using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadRoomCategoryDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên của hạng phòng")]
        public string Name { get; set; } = null!;

        public int? Order { get; set; }
    }
}
