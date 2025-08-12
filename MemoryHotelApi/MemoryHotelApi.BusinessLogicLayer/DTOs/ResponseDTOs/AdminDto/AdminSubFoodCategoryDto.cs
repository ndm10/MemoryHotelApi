using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class AdminSubFoodCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid FoodCategoryId { get; set; }
        public string? Description { get; set; }
        public FoodCategoryDtoCommon FoodCategory { get; set; } = new FoodCategoryDtoCommon();
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
