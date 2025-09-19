using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetStoriesExploreDto : GenericResponsePaginationDto<StoryExploreDto>
    {
    }

    public class StoryExploreDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Link { get; set; }
        public string? Description { get; set; }
    }
}
