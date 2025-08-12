namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<string> LocationHighlights { get; set; } = null!;
        public List<string> SuitableFor { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public string BranchLocation { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string HotelCode { get; set; } = null!;
        public string HotLine { get; set; } = null!;
        public ICollection<BranchImage> BranchImages { get; set; } = new List<BranchImage>();
        public ICollection<Convenience> GeneralConveniences { get; set; } = new List<Convenience>();
        public ICollection<Convenience> HighlightedConveniences { get; set; } = new List<Convenience>();
        public ICollection<LocationExplore> LocationExplores { get; set; } = new List<LocationExplore>();
        public ICollection<RoomCategory> RoomCategories { get; set; } = new List<RoomCategory>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<BranchReceptionist> BranchReceptionists { get; set; } = new List<BranchReceptionist>();
        public ICollection<FoodOrderHistory> FoodOrderHistories { get; set; } = new List<FoodOrderHistory>();
    }
}