using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetCitiesDto : GenericResponseDto
    {
        public List<GetCityDto>? Data { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
    }

    public class GetCityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int Order { get; set; }
        public string RegionKey { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
