using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBannerService
    {
        Task<ResponseGetBannerDto> GetBannerAsync(Guid id);
        Task<ResponseGetBannersDto> GetBannersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<GenericResponseDto> SoftDeleteAsync(Guid id);
        Task<GenericResponseDto> UpdateBannerAsync(RequestUpdateBannerDto request, Guid id);
        Task<GenericResponseDto> UploadBannerAsync(RequestUploadBannerDto request);
    }
}
