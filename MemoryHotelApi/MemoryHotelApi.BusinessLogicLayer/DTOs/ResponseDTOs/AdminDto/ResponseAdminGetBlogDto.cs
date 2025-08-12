using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseAdminGetBlogDto : BaseResponseDto
    {
        public AdminBlogDto? Data { get; set; }
    }
}
