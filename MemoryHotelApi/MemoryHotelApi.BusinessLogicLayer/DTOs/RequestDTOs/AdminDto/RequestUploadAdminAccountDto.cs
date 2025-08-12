using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadAdminAccountDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Nationality { get; set; }
    }
}
