namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class AdminFoodCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string? Description { get; set; }
        public List<AdminSubFoodCategoryDto> SubFoodCategories { get; set; } = new List<AdminSubFoodCategoryDto>();
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
