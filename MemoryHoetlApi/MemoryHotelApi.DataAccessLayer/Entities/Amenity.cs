namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Amenity: GenericEntity
    {
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
