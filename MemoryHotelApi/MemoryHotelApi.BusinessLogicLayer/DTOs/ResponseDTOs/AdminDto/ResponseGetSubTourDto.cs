using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetSubTourDto : BaseResponseDto
    {
        public GetSubTourDto? Data { get; set; }
    }
}
