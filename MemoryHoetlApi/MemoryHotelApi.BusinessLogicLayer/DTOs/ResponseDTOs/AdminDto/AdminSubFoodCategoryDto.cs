using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class AdminSubFoodCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public FoodCategory Category { get; set; } = new FoodCategory();
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
