namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class GenericEntity
    {
        public required Guid Id { get; set; }
        public required bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
