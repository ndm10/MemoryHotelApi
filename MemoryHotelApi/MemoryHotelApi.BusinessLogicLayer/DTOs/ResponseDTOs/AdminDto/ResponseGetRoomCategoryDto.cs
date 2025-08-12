using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetRoomCategoryDto : BaseResponseDto
    {
        public RoomCategoryDto? Data { get; set; }
    }
}
