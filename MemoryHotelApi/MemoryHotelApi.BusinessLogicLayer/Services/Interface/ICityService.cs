using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ICityService
    {
        Task<ResponseGetCityDto> GetCity(Guid id);
        Task<ResponseGetCitiesDto> GetCities(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteCityAsync(Guid id);
        Task<BaseResponseDto> UpdateCityAsync(RequestUpdateCityDto request, Guid id);
        Task<BaseResponseDto> UploadCityAsync(RequestUploadCityDto request);
        Task<ResponseGetCitiesExploreDto> GetCitiesExploreAsync();
    }
}
