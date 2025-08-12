using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetSubFoodCategoryExploreDto : BaseResponseDto
    {
        public ExploreSubFoodCategoryDto? Data { get; set; }
    }
}
