using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetBannerDto : GenericResponseDto
    {
        public GetBannerDto? Data { get; set; }
    }
}
