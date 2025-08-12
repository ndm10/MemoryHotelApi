
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseGetUserDto> GetUserAsync(Guid id);
        Task<ResponseGetUsersDto> GetUsersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, string roleName);
        Task<BaseResponseDto> SoftDeleteAsync(Guid id);
        Task<BaseResponseDto> UpdateAdminAccount(RequestUpdateAdminAccountDto request, Guid id);
        Task<BaseResponseDto> UpdateMembershipTierAsync(DTOs.RequestDTOs.AdminDto.RequestUpdateMembershipTierOfMemberDto request, Guid id);
        Task<BaseResponseDto> UploadAdminAccount(RequestUploadAdminAccountDto request);
    }
}
