using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseAdminGetFoodDto : BaseResponseDto
    {
        public AdminFoodDto? Data { get; set; }
    }
}
