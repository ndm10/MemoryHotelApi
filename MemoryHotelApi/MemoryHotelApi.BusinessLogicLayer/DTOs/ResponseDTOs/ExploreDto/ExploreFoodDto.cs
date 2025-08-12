using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ExploreFoodDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public bool? IsBestSeller { get; set; }
        public int? WaitingTimeInMinute { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public Guid SubFoodCategoryId { get; set; }
    }
}
