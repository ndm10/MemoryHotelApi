using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetToursDto : BaseResponseDto
    {
        public List<GetTourDto>? Data { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
    }

    public class GetTourDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string>? Images { get; set; }
        public List<GetSubTourDto>? SubTours { get; set; }
        public ResponseGetCityCommonDto? City { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
    }
}
