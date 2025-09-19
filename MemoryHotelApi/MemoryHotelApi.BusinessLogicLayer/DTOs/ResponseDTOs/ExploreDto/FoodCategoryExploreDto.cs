using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class FoodCategoryExploreDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public List<ResponseSubFoodCategoryCommonDto>? SubFoodCategories { get; set; }
        public int? Order { get; set; }
    }
}
