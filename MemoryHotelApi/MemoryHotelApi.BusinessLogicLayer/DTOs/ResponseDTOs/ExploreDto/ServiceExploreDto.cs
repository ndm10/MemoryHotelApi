using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ServiceExploreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceCategoryExploreDto ServiceCategory { get; set; } = null!;
        public int Order { get; set; }
    }
}