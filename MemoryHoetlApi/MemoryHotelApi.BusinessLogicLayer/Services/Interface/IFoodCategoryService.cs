using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IFoodCategoryService : IGenericService<FoodCategory>
    {
        Task<ResponseAdminGetFoodCategoriesDto> GetFoodCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseGetFoodCategoriesExploreDto> GetFoodCategoriesExploreAsync();
        Task<ResponseAdminGetFoodCategoryDto> GetFoodCategoryAsync(Guid id);
        Task<ResponseGetFoodCategoryExploreDto> GetFoodCategoryExploreAsync(Guid id);
        Task<BaseResponseDto> SoftDeleteFoodCategoryAsync(Guid id);
        Task<BaseResponseDto> UpdateFoodCategoryAsync(Guid id, RequestUpdateFoodCategoryDto request);
        Task<BaseResponseDto> UploadFoodCategoryAsync(RequestUploadFoodCategoryDto request);
    }
}
