namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class RoomCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
