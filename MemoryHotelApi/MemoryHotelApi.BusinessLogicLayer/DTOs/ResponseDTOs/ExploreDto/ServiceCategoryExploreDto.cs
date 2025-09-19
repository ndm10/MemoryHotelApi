using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ServiceCategoryExploreDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
