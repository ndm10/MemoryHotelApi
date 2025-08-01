namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class SubFoodCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public FoodCategory Category { get; set; } = new FoodCategory();
    }
}
