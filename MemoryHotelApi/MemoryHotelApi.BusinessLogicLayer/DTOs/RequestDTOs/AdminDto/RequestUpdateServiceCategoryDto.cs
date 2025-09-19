namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateServiceCategoryDto
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
    }
}
