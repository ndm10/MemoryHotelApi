namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class GenericResponsePaginationDto<T>
    {
        public int StatusCode { get; set; }
        public List<T>? Data { get; set; }
        public int? TotalPage { get; set; }
        public int? TotalRecord { get; set; }
        public string? Message { get; set; }
    }
}
