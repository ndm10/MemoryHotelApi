using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetFoodCategoryExploreDto : BaseResponseDto
    {
        public FoodCategoryExploreDto? Data { get; set; }
    }
}
