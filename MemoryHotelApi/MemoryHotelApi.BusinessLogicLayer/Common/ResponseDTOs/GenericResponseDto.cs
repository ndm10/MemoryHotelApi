namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class GenericResponseDto<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
