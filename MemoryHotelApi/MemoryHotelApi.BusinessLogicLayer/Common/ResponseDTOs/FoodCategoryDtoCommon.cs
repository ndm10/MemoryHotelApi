namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class FoodCategoryDtoCommon
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string? Description { get; set; }
    }
}
