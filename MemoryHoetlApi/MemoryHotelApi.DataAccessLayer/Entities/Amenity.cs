namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Amenity : BaseEntity
    {
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Description { get; set; }
        public int Order { get; set; }
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
