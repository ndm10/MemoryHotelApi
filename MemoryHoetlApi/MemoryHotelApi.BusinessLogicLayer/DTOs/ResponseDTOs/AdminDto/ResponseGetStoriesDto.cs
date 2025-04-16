using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetStoriesDto : GenericResponseDto
    {
        public List<GetStoryDto>? Data { get; set; }
        public int? TotalPage { get; set; }
        public int? TotalRecord { get; set; }
    }

    public class GetStoryDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Link { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
