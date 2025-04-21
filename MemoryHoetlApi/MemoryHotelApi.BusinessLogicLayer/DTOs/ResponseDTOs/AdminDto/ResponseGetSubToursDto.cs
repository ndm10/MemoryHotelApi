using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetSubToursDto : GenericResponseDto
    {
        public List<GetSubTourDto>? Data { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetSubTourDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string DepartureTime { get; set; } = null!;
        public int Duration { get; set; }
        public string Transportation { get; set; } = null!;
        public string TravelSchedule { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Guid TourId { get; set; }
        public List<string>? Images { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public TourDetailDto? Tour { get; set; }
    }

    public class TourDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
    }
}
