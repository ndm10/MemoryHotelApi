
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBranchService
    {
        Task<ResponseGetBranchDto> GetBranchAsync(Guid id);
        Task<ResponseGetBranchesDto> GetBranchesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteBranchAsync(Guid id);
        Task<BaseResponseDto> UpdateBranchAsync(RequestUpdateBranchDto request, Guid id);
        Task<BaseResponseDto> UploadBranchAsync(RequestUploadBranchDto request);
    }
}
