namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; } = null!;
    }
}
