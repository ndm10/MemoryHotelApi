
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ITourService
    {
        Task<ResponseGetTourDto> GetTourAsync(Guid id);
        Task<ResponseGetTourExploreDto> GetTourExploreAsync(Guid id);
        Task<ResponseGetToursDto> GetToursAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? cityId);
        Task<BaseResponseDto> SoftDeleteTourAsync(Guid id);
        Task<BaseResponseDto> UpdateTourAsync(RequestUpdateTourDto request, Guid id);
        Task<BaseResponseDto> UploadTourAsync(RequestUploadTourDto request);
    }
}
