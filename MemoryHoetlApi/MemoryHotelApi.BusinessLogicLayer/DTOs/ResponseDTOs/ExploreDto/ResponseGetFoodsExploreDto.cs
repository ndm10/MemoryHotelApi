using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetFoodsExploreDto : BaseResponseDto
    {
        public List<ExploreFoodDto>? Data { get; set; }
    }
}
