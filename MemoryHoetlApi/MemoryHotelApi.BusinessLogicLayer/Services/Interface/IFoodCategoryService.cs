using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IFoodCategoryService : IGenericService<FoodCategory>
    {
        Task<ResponseAdminGetFoodCategoriesDto> GetFoodCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<ResponseAdminGetFoodCategoryDto> GetFoodCategoryAsync(Guid id);
        Task<BaseResponseDto> SoftDeleteFoodCategoryAsync(Guid id);
        Task<BaseResponseDto> UpdateFoodCategoryAsync(Guid id, RequestUpdateFoodCategoryDto request);
        Task<BaseResponseDto> UploadFoodCategoryAsync(RequestUploadFoodCategoryDto request);
    }
}
