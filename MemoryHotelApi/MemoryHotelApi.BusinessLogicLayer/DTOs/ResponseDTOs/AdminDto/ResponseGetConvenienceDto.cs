using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetConvenienceDto : BaseResponseDto
    {
        public ConvenienceDto? Data { get; set; }
    }
}
