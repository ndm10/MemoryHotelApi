namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class BranchImage
    {
        public Guid BranchId { get; set; }
        public Guid ImageId { get; set; }
        public int Order { get; set; }
        public Branch Branch { get; set; } = null!;
        public Image Image { get; set; } = null!;
    }
}
