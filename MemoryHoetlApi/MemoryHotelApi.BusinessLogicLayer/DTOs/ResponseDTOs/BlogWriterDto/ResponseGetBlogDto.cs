using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.BlogWriterDto
{
    public class ResponseGetBlogDto : BaseResponseDto
    {
        public BlogDto? Data { get; set; }
    }
}
