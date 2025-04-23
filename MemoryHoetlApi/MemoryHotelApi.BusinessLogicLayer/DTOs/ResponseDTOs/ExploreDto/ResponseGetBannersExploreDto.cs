using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetBannersExploreDto : BaseResponseDto
    {
        public List<GetBannerDto>? Data { get; set; }
    }

    public class GetBannersExploreDto
    {
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
    }
}
