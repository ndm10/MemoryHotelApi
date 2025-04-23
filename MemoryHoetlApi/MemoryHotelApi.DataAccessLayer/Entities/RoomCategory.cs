namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class RoomCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
