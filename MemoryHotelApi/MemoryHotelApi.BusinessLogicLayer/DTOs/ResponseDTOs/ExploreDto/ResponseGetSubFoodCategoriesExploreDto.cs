using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetSubFoodCategoriesExploreDto : BaseResponseDto
    {
        public List<ExploreSubFoodCategoryDto>? Data { get; set; }
    }
}
