namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class City : GenericEntity
    {
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public int Order { get; set; }
        public string RegionKey { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
