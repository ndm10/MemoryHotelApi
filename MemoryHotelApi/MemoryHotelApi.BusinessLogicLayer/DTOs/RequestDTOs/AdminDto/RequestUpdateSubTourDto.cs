using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateSubTourDto
    {
        public string? Title { get; set; }
        public string? DepartureTime { get; set; }
        public int? Duration { get; set; }
        public string? Transportation { get; set; }
        public string? TravelSchedule { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ")]
        public decimal? Price { get; set; }

        public Guid? TourId { get; set; }
        public List<string>? ImageUrls { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
    }
}
