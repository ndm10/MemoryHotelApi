namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!;
        public string Location { get; set; } = null!;
        public double MinutesToRead { get; set; }
        public bool IsPopular { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;
        public ICollection<BlogHashTag> BlogHashtags { get; set; } = new List<BlogHashTag>();
    }
}
