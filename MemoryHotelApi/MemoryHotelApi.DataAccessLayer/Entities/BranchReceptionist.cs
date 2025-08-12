namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class BranchReceptionist
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = new Branch();
    }
}
