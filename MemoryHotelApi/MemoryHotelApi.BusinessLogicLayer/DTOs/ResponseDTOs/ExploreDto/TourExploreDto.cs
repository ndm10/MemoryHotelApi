using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class TourExploreDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Slug { get; set; }
        public ICollection<string>? Images { get; set; }
        public ICollection<SubTourExploreDto>? SubTours { get; set; }
    }
}
