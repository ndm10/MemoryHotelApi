using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetBranchExploreDto : BaseResponseDto
    {
        public GetBranchesExploreDto? Data { get; set; }
    }
}
