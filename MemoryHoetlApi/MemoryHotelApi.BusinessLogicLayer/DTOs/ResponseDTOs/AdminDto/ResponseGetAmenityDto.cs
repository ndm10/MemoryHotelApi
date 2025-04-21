using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetAmenityDto : BaseResponseDto
    {
        public GetAmenityDto? Data { get; set; }
    }
}
