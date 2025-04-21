namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class BaseResponseDto
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Error? Errors { get; set; }
    }

    public class Error
    {
        public List<string>? Messages { get; set; }
    }
}
