using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetFoodCategoryExploreDto : BaseResponseDto
    {
        public ExploreFoodCategoryDto? Data { get; set; }
    }
}
