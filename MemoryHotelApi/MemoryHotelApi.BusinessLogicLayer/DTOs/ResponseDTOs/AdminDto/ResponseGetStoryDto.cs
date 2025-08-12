using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetStoryDto : BaseResponseDto
    {
        public GetStoryDto? Data { get; set; }
    }
}
