namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Story : GenericEntity
    {
        public string ImageUrl { get; set; } = null!;
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
