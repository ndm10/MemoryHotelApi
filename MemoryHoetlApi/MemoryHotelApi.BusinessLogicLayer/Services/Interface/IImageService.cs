using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ImageDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IImageService : IGenericService<Image>
    {
        Task<ResponseImageUploadDto> UploadImage(List<RequestImageUploadDto> imageDtos, string localRootPath, string urlPath);
    }
}
