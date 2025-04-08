namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class GenericEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
