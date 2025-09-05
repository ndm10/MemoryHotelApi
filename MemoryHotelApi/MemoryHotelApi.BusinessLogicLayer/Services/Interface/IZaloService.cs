using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ZaloDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IZaloService
    {
        public Task<ResponseSendTextMessageGroupChatZaloDto> SendMessageToZaloGroupChatAsync(FoodOrderHistory foodOrderHistory);
    }
}
