
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseGetMembershipDto> GetMembershipAsync(Guid id);
        Task<ResponseGetMembershipsDto> GetMembershipsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
    }
}
