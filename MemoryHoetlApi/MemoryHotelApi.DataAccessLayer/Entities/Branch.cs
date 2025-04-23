namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<string> LocationHighlights { get; set; } = null!;
        public List<string> SuitableFor { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Convenience> GeneralConveniences { get; set; } = new List<Convenience>();
        public ICollection<Convenience> HighlightedConveniences { get; set; } = new List<Convenience>();
        public ICollection<LocationExplore> LocationExplores { get; set; } = new List<LocationExplore>();
    }
}
