using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBannerService
    {
        Task<ResponseGetBannerDto> GetBannerAsync(Guid id);
        Task<ResponseGetBannersExploreDto> GetAllBannersAsync();
        Task<ResponseGetBannersDto> GetBannersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteAsync(Guid id);
        Task<BaseResponseDto> UpdateBannerAsync(RequestUpdateBannerDto request, Guid id);
        Task<BaseResponseDto> UploadBannerAsync(RequestUploadBannerDto request);
    }
}
