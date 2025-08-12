using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateCityDto
    {
        public string? Name { get; set; }
        public string? Region { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
