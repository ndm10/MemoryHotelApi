namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Image : BaseEntity
    {
        public string Url { get; set; } = null!;
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
        public ICollection<SubTour> SubTours { get; set; } = new List<SubTour>();
        public ICollection<BranchImage> BranchImages { get; set; } = new List<BranchImage>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
