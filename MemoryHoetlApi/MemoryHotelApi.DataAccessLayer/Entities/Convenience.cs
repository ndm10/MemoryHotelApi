namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Convenience : BaseEntity
    {
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Branch> BranchesWithGeneralConvenience { get; set; } = new List<Branch>();
        public ICollection<Branch> BranchesWithHighlightedConvenience { get; set; } = new List<Branch>();
    }
}
