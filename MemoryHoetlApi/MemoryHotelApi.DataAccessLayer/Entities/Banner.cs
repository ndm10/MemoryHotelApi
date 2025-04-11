namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Banner : GenericEntity
    {
        public required string ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
