using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.BlogWriterDto
{
    public class RequestUpdateBlogDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        [Range(1, 60, ErrorMessage = "Thời gian đọc phải từ 1 đến 60 phút")]
        public double? MinutesToRead { get; set; }

        public int? Order { get; set; }
        public bool? IsActive { get; set; }
        public List<string>? Hashtag { get; set; }
        public string? Location { get; set; }
        public bool? IsPopular { get; set; }
    }
}
