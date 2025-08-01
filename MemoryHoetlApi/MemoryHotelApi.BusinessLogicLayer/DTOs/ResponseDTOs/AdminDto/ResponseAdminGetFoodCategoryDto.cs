using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseAdminGetFoodCategoryDto : BaseResponseDto
    {
        public AdminFoodCategoryDto? Data { get; set; }
    }
}
