using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto
{
    public class ResponseImageUploadDto : GenericResponseDto
    {
        public List<string> Urls { get; set; } = null!;
    }
}
