namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Tour : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public Guid CityId { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<SubTour> SubTours { get; set; } = new List<SubTour>();
        public City City { get; set; } = null!;
    }
}
