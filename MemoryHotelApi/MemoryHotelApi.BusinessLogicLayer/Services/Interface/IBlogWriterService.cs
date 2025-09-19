using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IBlogWriterService
    {
        Task<GenericResponsePaginationDto<BlogDto>> GetBlogsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid authorId);
        Task<ResponseGetBlogDto> GetBlogByIdAsync(Guid id);
        Task<BaseResponseDto> CreateBlogAsync(RequestCreateBlogDto blogDto, Guid authorId);
        Task<BaseResponseDto> UpdateBlogAsync(Guid id, RequestUpdateBlogDto blogDto);
        Task<BaseResponseDto> DeleteBlogAsync(Guid id);
        Task<GenericResponsePaginationDto<BlogExploreDto>> GetBlogsExploreAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetBlogExploreDto> GetBlogExploreAsync(Guid id);
        Task<BaseResponseDto> UpdateBlogByAdminAsync(Guid blogId, RequestAdminUpdateBlogDto blogDto);
        Task<GenericResponsePaginationDto<AdminBlogDto>> GetBlogsAdminAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseAdminGetBlogDto> GetBlogByIdAdminAsync(Guid id);
    }
}
