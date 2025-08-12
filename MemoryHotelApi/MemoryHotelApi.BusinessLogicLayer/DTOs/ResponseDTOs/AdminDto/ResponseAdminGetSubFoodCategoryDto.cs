using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseAdminGetSubFoodCategoryDto : BaseResponseDto
    {
        public AdminSubFoodCategoryDto? Data { get; set; }
    }
}
