using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IServiceCategoryService : IGenericService<ServiceCategory>
    {
        Task<ResponseAdminGetServiceCategoryDto> GetServiceCategoryAsync(Guid id);
        Task<ResponseGetServiceCategoryExploreDto> GetServiceCategoryExploreAsync(Guid id);
        Task<ResponseAdminGetServiceCategoriesDto> GetServiceCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetServiceCategoriesExploreDto> GetServiceCategoriesExploreAsync(int? pageIndex, int? pageSize, string? textSearch);
        Task<BaseResponseDto> SoftDeleteServiceCategoryAsync(Guid id);
        Task<BaseResponseDto> UpdateServiceCategoryAsync(Guid id, RequestUpdateServiceCategoryDto request);
        Task<BaseResponseDto> UploadServiceCategoryAsync(RequestUploadServiceCategoryDto request);
    }
}
