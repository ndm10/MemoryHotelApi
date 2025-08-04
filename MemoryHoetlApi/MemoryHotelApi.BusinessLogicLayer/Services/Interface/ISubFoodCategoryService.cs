using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface ISubFoodCategoryService : IGenericService<SubFoodCategory>
    {
        Task<ResponseAdminGetSubFoodCategoriesDto> GetSubFoodCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? foodCategoryId);
        Task<ResponseGetSubFoodCategoriesExploreDto> GetSubFoodCategoriesExploreAsync(Guid? foodCategoryId);
        Task<ResponseAdminGetSubFoodCategoryDto> GetSubFoodCategoryAsync(Guid id);
        Task<ResponseGetSubFoodCategoryExploreDto> GetSubFoodCategoryExploreAsync(Guid id);
        Task<BaseResponseDto> SoftDeleteSubFoodCategoryAsync(Guid id);
        Task<BaseResponseDto> UpdateSubFoodCategoryAsync(Guid id, RequestUpdateSubFoodCategoryDto request);
        Task<BaseResponseDto> UploadSubFoodCategoryAsync(RequestUploadSubFoodCategoryDto request);
    }
}
