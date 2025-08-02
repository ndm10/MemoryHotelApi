namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateFoodDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public bool? IsBestSeller { get; set; }
        public int? WaitingTimeInMinute { get; set; }
        public string? Description { get; set; }
        public Guid? SubFoodCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? Order { get; set; }
    }
}
