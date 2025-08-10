using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.Receptionist
{
    public class FoodOrderHistoryDto
    {
        public Guid Id { get; set; }
        public string Room { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string? Note { get; set; } = null!;
        public string Status { get; set; } = null!;
        public List<FoodOrderHistoryDetailDto>? Items { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal TotalBill { get; set; }
    }

    public class FoodOrderHistoryDetailDto
    {
        public Guid Id { get; set; }
        public Guid FoodId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
