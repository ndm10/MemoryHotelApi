using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetCityDto : BaseResponseDto
    {
        public GetCityDto? Data { get; set; }
    }
}
