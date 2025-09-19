namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class MotorcycleRentalHistory: BaseEntity
    {
        public string RentalCode { get; set; } = null!;
        public Guid BranchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Room { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; }
        public string? ReceptionistName { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
        ICollection<MotorcycleRentalHistoryDetail> Items { get; set; } = new List<MotorcycleRentalHistoryDetail>();
        public Branch Branch { get; set; } = null!;
    }
}
