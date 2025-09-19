namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class CarBookingHistoryDetail : BaseEntity
    {
        public Guid CarBookingHistoryId { get; set; }
        public CarBookingHistory CarBookingHistory { get; set; } = null!;
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
