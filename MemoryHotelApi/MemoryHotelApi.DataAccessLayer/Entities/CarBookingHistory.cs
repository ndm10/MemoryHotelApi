namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class CarBookingHistory : BaseEntity
    {
        public string BookingCode { get; set; } = null!;
        public string PickupLocation { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string Room { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; }
        public string? ReceptionistName { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
        ICollection<CarBookingHistoryDetail> Items { get; set; } = new List<CarBookingHistoryDetail>();
    }
}
