
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IAmenityService
    {
        Task<ResponseGetAmenitiesDto> GetAmenities(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetAmenityDto> GetAmenity(Guid id);
        Task<BaseResponseDto> SoftDeleteAmenityAsync(Guid id);
        Task<BaseResponseDto> UpdateAmenityAsync(RequestUpdateAmenityDto request, Guid id);
        Task<BaseResponseDto> UploadAmenityAsync(RequestUploadAmenityDto request);
    }
}
