using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBannerService
    {
        Task<GenericResponseDto> SoftDeleteAsync(Guid id);
        Task<GenericResponseDto> UploadBanner(RequestUploadBannerDto request);
    }
}
