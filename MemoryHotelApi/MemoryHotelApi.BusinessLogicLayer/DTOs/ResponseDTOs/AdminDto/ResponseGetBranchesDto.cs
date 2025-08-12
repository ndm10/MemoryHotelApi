using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetBranchesDto : GenericResponsePagination<BranchDto>
    {
    }

    public class BranchDto
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
        public bool? IsActive { get; set; }
        public List<string>? Images { get; set; }
        public List<ConvenienceDto>? GeneralConveniences { get; set; }
        public List<ConvenienceDto>? HighlightedConveniences { get; set; }
        public ICollection<ResponseGetLocationExploreDtoCommon>? LocationExplores { get; set; }
        public ICollection<RoomCategoryDto>? RoomCategories { get; set; }
        public string? HotLine { get; set; }
    }
}
