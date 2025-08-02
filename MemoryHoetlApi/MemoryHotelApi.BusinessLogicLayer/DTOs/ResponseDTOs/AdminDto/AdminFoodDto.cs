using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class AdminFoodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public bool IsBestSeller { get; set; }
        public int WaitingTimeInMinute { get; set; }
        public string? Description { get; set; }
        public Guid SubFoodCategoryId { get; set; }
        public AdminSubFoodCategoryDto SubFoodCategory { get; set; } = null!;
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
