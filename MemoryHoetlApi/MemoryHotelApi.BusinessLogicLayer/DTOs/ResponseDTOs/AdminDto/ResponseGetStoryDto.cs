using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetStoryDto : GenericResponseDto
    {
        public GetStoryDto? Data { get; set; }
    }
}
