namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class FoodOrderHistory : BaseEntity
    {
        public Guid BranchId { get; set; }
        public string Room { get; set; } = null!;
        public string customerPhone { get; set; } = null!;
        public string? Note { get; set; } = null!;
        public int Status {  get; set; }
        public ICollection<FoodOrderHistoryDetail> Items { get; set; } = new List<FoodOrderHistoryDetail>();
    }
}
