using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist
{
    public class RequestCreateOrderDto
    {
        [Required(ErrorMessage = "Please choose branch")]
        public Guid BranchId { get; set; }

        [Required(ErrorMessage = "Please enter room number")]
        public string Room { get; set; } = null!;

        [Required(ErrorMessage = "Please enter customer phone")]
        public string CustomerPhone { get; set; } = null!;

        public List<FoodItems> Items { get; set; } = new List<FoodItems>();
        public string? Note { get; set; }
        public string Status { get; set; } = null!;
    }

    public class FoodItems
    {
        public Guid Id { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Please enter quantity greater than 0")]
        public int quantity { get; set; }
    }
}
