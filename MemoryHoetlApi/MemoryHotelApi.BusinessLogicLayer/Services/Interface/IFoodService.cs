using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IFoodService : IGenericService<Food>
    {
        Task<ResponseAdminGetFoodDto> GetFoodAsync(Guid id);
        Task<ResponseGetFoodExploreDto> GetFoodExploreAsync(Guid id);
        Task<ResponseAdminGetFoodsDto> GetFoodsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? subFoodCategoryId);
        Task<ResponseGetFoodsExploreDto> GetFoodsExploreAsync(string? textSearch, Guid? subFoodCategoryId);
        Task<BaseResponseDto> SoftDeleteFoodAsync(Guid id);
        Task<BaseResponseDto> UpdateFoodAsync(Guid id, RequestUpdateFoodDto request);
        Task<BaseResponseDto> UploadFoodAsync(RequestUploadFoodDto request);
    }
}
