namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class SubTour : GenericEntity
    {
        public string Title { get; set; } = null!;
        public string DepartureTime { get; set; } = null!;
        public int Duration { get; set; }
        public string Transportation { get; set; } = null!;
        public string TravelSchedule { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid TourId { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public Tour Tour { get; set; } = null!;
    }
}
