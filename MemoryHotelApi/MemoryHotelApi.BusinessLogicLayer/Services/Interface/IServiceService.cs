using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IServiceService : IGenericService<Service>
    {
        Task<ResponseAdminGetServiceDto> GetServiceAsync(Guid id);
        Task<ResponseGetServiceExploreDto> GetServiceExploreAsync(Guid id);
        Task<ResponseAdminGetServicesDto> GetServicesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? serviceCategoryId);
        Task<ResponseGetServicesExploreDto> GetServicesExploreAsync(int? pageIndex, int? pageSize, string? textSearch, Guid? serviceCategoryId);
        Task<BaseResponseDto> SoftDeleteServiceAsync(Guid id);
        Task<BaseResponseDto> UpdateServiceAsync(Guid id, RequestUpdateServiceDto dto);
        Task<BaseResponseDto> UploadServiceAsync(RequestUploadServiceDto dto);
    }
}
