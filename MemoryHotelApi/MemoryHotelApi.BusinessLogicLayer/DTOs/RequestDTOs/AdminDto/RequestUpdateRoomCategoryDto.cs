using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateRoomCategoryDto
    {
        public string? Name { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
    }
}
