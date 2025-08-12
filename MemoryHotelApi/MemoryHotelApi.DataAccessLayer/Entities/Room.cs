namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Area { get; set; }
        public string BedType { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public Guid RoomCategoryId { get; set; }
        public Guid BranchId { get; set; }
        public ICollection<Convenience> Conveniences { get; set; } = new List<Convenience>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public RoomCategory RoomCategory { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
    }
}
