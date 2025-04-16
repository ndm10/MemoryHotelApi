using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
