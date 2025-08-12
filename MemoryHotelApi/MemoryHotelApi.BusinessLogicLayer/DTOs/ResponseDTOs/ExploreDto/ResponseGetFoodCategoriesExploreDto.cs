using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetFoodCategoriesExploreDto : BaseResponseDto
    {
        public List<ExploreFoodCategoryDto>? Data { get; set; }
    }
}
