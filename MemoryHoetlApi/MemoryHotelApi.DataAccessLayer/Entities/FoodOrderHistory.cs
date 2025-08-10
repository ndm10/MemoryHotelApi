namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class FoodOrderHistory : BaseEntity
    {
        public string OrderCode { get; set; } = null!;
        public Guid BranchId { get; set; }
        public string Room { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string? Note { get; set; }
        public int Status {  get; set; }
        public ICollection<FoodOrderHistoryDetail> Items { get; set; } = new List<FoodOrderHistoryDetail>();
        public Branch Branch { get; set; } = null!;
    }
}
