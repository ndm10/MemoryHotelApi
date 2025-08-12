
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IConvenienceService
    {
        Task<ResponseGetConveniencesDto> GetConveniencesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetConvenienceDto> GetConvenienceAsync(Guid id);
        Task<BaseResponseDto> SoftDeleteConvenienceAsync(Guid id);
        Task<BaseResponseDto> UpdateConvenienceAsync(RequestUpdateConvenienceDto request, Guid id);
        Task<BaseResponseDto> UploadConvenienceAsync(RequestUploadConvenienceDto request);
    }
}
