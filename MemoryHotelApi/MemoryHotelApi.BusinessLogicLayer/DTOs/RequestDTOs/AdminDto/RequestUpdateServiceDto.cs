namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateServiceDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public Guid? ServiceCategoryId { get; set; }
        public string? Image { get; set; }
        public bool? IsActive { get; set; }
    }
}
