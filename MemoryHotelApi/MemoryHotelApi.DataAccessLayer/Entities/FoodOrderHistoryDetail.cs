namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class FoodOrderHistoryDetail: BaseEntity
    {
        public Guid FoodOrderHistoryId { get; set; }
        public FoodOrderHistory FoodOrderHistory { get; set; } = null!;
        public Guid FoodId { get; set; }
        public Food Food { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
