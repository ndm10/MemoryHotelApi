namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class FoodCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<SubFoodCategory> SubFoodCategories { get; set; } = new List<SubFoodCategory>();
    }
}
