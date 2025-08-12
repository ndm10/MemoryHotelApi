using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto
{
    public class ResponseImageUploadDto : BaseResponseDto
    {
        public List<string> Urls { get; set; } = null!;
    }
}
