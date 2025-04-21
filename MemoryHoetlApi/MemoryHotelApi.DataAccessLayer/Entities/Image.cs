namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Image : GenericEntity
    {
        public string Url { get; set; } = null!;
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
        public ICollection<SubTour> SubTours { get; set; } = new List<SubTour>();
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
