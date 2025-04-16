
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ITourService
    {
        Task<ResponseGetTourDto> GetTourAsync(Guid id);
        Task<ResponseGetToursDto> GetToursAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? cityId);
        Task<GenericResponseDto> SoftDeleteTourAsync(Guid id);
        Task<GenericResponseDto> UpdateTourAsync(RequestUpdateTourDto request, Guid id);
        Task<GenericResponseDto> UploadTourAsync(RequestUploadTourDto request);
    }
}
