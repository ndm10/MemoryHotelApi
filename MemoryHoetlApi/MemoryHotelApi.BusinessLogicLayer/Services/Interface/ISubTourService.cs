
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ISubTourService
    {
        Task<ResponseGetSubTourDto> GetSubTourAsync(Guid id);
        Task<ResponseGetSubToursDto> GetSubToursAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? tourId);
        Task<GenericResponseDto> SoftDeleteSubTourAsync(Guid id);
        Task<GenericResponseDto> UpdateSubTourAsync(RequestUpdateSubTourDto request, Guid id);
        Task<GenericResponseDto> UploadSubTourAsync(RequestUploadSubTourDto request);
    }
}
