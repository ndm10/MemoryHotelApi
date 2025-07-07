using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.BlogWriterDto
{
    public class RequestCreateBlogDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập đường dẫn hình ảnh thumbnail")]
        public string Thumbnail { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mô tả bài viết")]
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [MinLength(10, ErrorMessage = "Mô tả phải có ít nhất 10 ký tự")]
        public string Description { get; set; } = null!;

        [Range(1, 60, ErrorMessage = "Thời gian đọc phải từ 1 đến 60 phút")]
        public double MinutesToRead { get; set; }

        public int? Order { get; set; }
        public string? Slug { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ít nhất một hashtag.")]
        [AtLeastOneHashtag(ErrorMessage = "Vui lòng nhập ít nhất một hashtag.")]
        public List<string> Hashtag { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập vị trí bài viết")]
        [MaxLength(100, ErrorMessage = "Vị trí không được vượt quá 100 ký tự")]
        public string Location { get; set; } = null!;

        public bool IsPopular { get; set; }

        public bool? IsEmailNotification { get; set; }
    }
}
