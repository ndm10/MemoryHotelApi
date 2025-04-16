using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetToursDto : GenericResponseDto
    {
        public List<GetTourDto>? Data { get; set; }
        public int TotalCount { get; set; }
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
        public GenericResponseGetCityDto? City { get; set; }
    }
}
