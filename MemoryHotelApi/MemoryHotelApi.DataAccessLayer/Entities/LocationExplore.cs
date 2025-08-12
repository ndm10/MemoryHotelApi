namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class LocationExplore : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Distance { get; set; } = null!;
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
