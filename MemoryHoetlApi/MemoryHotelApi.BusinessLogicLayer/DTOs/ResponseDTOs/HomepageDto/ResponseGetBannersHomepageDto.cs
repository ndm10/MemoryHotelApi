using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.HomepageDto
{
    public class ResponseGetBannersHomepageDto : BaseResponseDto
    {
        public List<GetBannerDto>? Data { get; set; }
    }

    public class GetBannersHomepageDto
    {
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
    }
}
