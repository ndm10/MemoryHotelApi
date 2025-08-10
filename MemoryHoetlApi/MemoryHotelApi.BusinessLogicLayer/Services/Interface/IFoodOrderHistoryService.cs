using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.Receptionist;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IFoodOrderHistoryService : IGenericService<FoodOrderHistory>
    {
        Task<BaseResponseDto> CreateOrderAsync(RequestCreateOrderDto request, Guid userId);
        Task<ResponseGetFoodOrderHistoriesDto> GetFoodOrderHistoriesAsync(int? pageIndex, int? pageSize, string? textSearch, string? orderStatus, Guid receptionistId);
        Task<ResponseGetFoodOrderHistoryDto> GetFoodOrderHistoryAsync(string userId, Guid id);
        Task<BaseResponseDto> UpdateOrderAsync(Guid id, RequestUpdateOrderDto request, Guid guid);
    }
}
