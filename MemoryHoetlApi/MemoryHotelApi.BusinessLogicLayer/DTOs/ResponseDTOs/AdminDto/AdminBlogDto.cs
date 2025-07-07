using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class AdminBlogDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!;
        public double MinutesToRead { get; set; }
        public string Location { get; set; } = null!;
        public List<string> Hashtag { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public UserDto Author { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsPopular { get; set; }
    }
}
