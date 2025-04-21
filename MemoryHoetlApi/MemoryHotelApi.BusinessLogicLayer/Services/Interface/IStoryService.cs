using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IStoryService
    {
        Task<ResponseGetStoryDto> GetStoryAsync(Guid id);
        Task<ResponseGetStoriesDto> GetStoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteStoryAsync(Guid id);
        Task<BaseResponseDto> UpdateStoryAsync(RequestUpdateStoryDto request, Guid id);
        Task<BaseResponseDto> UploadStoryAsync(RequestUploadStoryDto request);
    }
}
