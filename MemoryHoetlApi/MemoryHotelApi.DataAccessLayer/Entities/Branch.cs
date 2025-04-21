namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string LocationHighlights { get; set; } = null!;
        public string SuitableFor { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public string Description { get; set; } = null!;
        public int Order { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}
