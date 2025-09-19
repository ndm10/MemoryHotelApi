using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class SubFoodCategoryExploreDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; } = null!;
        public Guid? FoodCategoryId { get; set; }
        public string? Description { get; set; }
        public FoodCategoryCommonDto? FoodCategory { get; set; }
        public List<FoodExploreDto>? Foods { get; set; }
        public int? Order { get; set; }
    }
}
