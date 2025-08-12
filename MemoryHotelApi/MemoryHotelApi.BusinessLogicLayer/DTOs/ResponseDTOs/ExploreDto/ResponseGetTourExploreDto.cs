using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetTourExploreDto : BaseResponseDto
    {
        public TourExploreDto? Data { get; set; }
    }
}
