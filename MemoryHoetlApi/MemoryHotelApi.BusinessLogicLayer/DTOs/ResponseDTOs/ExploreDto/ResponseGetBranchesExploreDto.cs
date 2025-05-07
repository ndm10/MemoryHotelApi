using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetBranchesExploreDto : BaseResponseDto
    {
        public List<GetBranchesExploreDto>? Data { get; set; }
    }

    public class GetBranchesExploreDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<string>? LocationHighlights { get; set; }
        public List<string>? SuitableFor { get; set; }
        public decimal? PricePerNight { get; set; }
        public string? BranchLocation { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
        public string? HotelCode { get; set; }
        public List<string>? Images { get; set; }
        public List<GetConvenienceDtoCommon>? GeneralConveniences { get; set; }
        public List<GetConvenienceDtoCommon>? HighlightedConveniences { get; set; }
        public ICollection<ResponseGetLocationExploreDtoCommon>? LocationExplores { get; set; }
        public ICollection<RoomCategoryExploreDto>? RoomCategories { get; set; }
    }
}