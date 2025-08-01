namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Food : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public bool IsBestSeller { get; set; }
        public int WaitingTimeInMinute { get; set; }
        public string? Description { get; set; }
        public Guid SubFoodCategoryId { get; set; }
        public SubFoodCategory SubFoodCategory { get; set; } = null!;
        public ICollection<FoodOrderHistoryDetail> FoodOrderHistoryDetails { get; set; } = new List<FoodOrderHistoryDetail>();
    }
}
