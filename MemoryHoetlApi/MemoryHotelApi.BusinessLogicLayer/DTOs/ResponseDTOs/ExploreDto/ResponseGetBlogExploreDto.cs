using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetBlogExploreDto : BaseResponseDto
    {
        public BlogExploreDto? Data { get; set; }
    }
}
