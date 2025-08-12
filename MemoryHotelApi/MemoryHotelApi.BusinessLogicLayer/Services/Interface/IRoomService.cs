
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IRoomService
    {
        Task<ResponseGetRoomDto> GetRoomAsync(Guid id);
        Task<ResponseGetRoomsDto> GetRoomsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? branchId, Guid? roomCategoryId);
        Task<BaseResponseDto> SoftDeleteRoomAsync(Guid id);
        Task<BaseResponseDto> UpdateRoomAsync(RequestUpdateRoomDto request, Guid id);
        Task<BaseResponseDto> UploadRoomAsync(RequestUploadRoomDto request);
    }
}
