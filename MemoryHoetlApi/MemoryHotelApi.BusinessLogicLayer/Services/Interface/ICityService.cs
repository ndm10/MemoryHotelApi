using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ICityService
    {
        Task<ResponseGetCityDto> GetCity(Guid id);
        Task<ResponseGetCitiesDto> GetCities(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<GenericResponseDto> SoftDeleteCityAsync(Guid id);
        Task<GenericResponseDto> UpdateCityAsync(RequestUpdateCityDto request, Guid id);
        Task<GenericResponseDto> UploadCityAsync(RequestUploadCityDto request);
    }
}
