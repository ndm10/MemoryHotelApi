namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string RegionKey { get; set; } = null!;
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
