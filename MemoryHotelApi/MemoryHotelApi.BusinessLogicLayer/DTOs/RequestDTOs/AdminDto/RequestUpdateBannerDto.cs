using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateBannerDto
    {
        public string? Link { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
    }
}
