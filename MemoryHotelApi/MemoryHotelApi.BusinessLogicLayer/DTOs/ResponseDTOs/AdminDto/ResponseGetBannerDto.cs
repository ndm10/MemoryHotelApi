using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetBannerDto : BaseResponseDto
    {
        public GetBannerDto? Data { get; set; }
    }
}
