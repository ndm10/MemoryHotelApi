namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class RoomCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
