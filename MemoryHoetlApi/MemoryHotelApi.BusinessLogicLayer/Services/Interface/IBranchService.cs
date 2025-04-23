
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBranchService
    {
        Task<ResponseGetBranchDto> GetBranchAsync(Guid id);
        Task<ResponseGetBranchesDto> GetBranchesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetBranchesExploreDto> GetBranchesExploreAsync();
        Task<BaseResponseDto> SoftDeleteBranchAsync(Guid id);
        Task<BaseResponseDto> UpdateBranchAsync(RequestUpdateBranchDto request, Guid id);
        Task<BaseResponseDto> UploadBranchAsync(RequestUploadBranchDto request);
    }
}
