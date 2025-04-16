using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetCityDto : GenericResponseDto
    {
        public GetCityDto? Data { get; set; }
    }
}
