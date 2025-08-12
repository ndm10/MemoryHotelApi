namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class BlogHashTag
    {
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
        
        public Guid HashtagId { get; set; }
        public Hashtag Hashtag { get; set; } = null!;
    }
}
