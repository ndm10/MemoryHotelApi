using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetRoomDto : BaseResponseDto
    {
        public RoomDto? Data { get; set; }
    }
}
