
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IRoomCategoryService
    {
        Task<ResponseGetRoomCategoriesDto> GetRoomCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetRoomCategoryDto> GetRoomCategoryAsync(Guid id);
        Task<BaseResponseDto> UploadRoomCategoryAsync(RequestUploadRoomCategoryDto request);
        Task<BaseResponseDto> UpdateRoomCategoryAsync(RequestUpdateRoomCategoryDto request, Guid id);
        Task<BaseResponseDto> SoftDeleteRoomCategoryAsync(Guid id);
    }
}
