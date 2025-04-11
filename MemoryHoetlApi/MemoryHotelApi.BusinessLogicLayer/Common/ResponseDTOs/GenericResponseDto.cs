namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class GenericResponseDto
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public string? Message { get; set; }
    }
}
