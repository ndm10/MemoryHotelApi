using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class UserExploreDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
