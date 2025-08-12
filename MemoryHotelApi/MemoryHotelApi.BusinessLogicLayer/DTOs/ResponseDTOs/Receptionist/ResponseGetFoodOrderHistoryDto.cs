using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.Receptionist
{
    public class ResponseGetFoodOrderHistoryDto : BaseResponseDto
    {
        public FoodOrderHistoryDto? Data { get; set; }
    }
}
