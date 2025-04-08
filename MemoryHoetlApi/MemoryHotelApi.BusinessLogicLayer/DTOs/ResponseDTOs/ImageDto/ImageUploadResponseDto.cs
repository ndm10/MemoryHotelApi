using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto
{
    public class ImageUploadResponseDto : GenericResponseDto
    {
        public List<string> Urls { get; set; } = null!;
    }
}
