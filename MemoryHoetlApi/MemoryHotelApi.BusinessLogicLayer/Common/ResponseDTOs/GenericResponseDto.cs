namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class GenericResponseDto
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
