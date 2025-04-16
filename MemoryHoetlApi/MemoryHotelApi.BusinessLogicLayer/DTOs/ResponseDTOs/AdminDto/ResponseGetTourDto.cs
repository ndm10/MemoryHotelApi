using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetTourDto : GenericResponseDto
    {
        public GetTourDto? Data { get; set; }
    }
}
