namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Hashtag : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<BlogHashTag> BlogHashtags { get; set; } = new List<BlogHashTag>();
    }
}
