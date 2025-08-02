using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetFoodExploreDto : BaseResponseDto
    {
        public ExploreFoodDto? Data { get; set; }
    }
}
