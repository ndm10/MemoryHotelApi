
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IMembershipTierService
    {
        Task<ResponseGetMembershipTierDto> GetMembershipTierAsync(Guid id);
        Task<ResponseGetMembershipTiersDto> GetMembershipTiersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteMembershipTierAsync(Guid id);
        Task<BaseResponseDto> UpdateMembershipTierAsync(RequestUpdateMembershipTierDto request, Guid id);
        Task<BaseResponseDto> UploadMembershipTierAsync(RequestUploadMembershipTierDto request);
    }
}
