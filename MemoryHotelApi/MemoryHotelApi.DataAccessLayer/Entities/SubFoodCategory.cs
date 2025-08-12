namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class SubFoodCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public Guid FoodCategoryId { get; set; }
        public string? Description { get; set; }
        public FoodCategory FoodCategory { get; set; } = new FoodCategory();
        public ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
