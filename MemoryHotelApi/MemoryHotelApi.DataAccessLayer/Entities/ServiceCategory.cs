namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string? Description { get; set; }
    }
}
