using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class SubTourExploreDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string DepartureTime { get; set; } = null!;
        public int Duration { get; set; }
        public string Transportation { get; set; } = null!;
        public string TravelSchedule { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public ICollection<string> Images { get; set; } = new List<string>();
    }
}
