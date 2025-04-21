namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Story : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
    }
}
