using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto
{
    public class RequestMotorcycleRentalDto
    {
        [Required(ErrorMessage = "Please choose choose branch")]
        public Guid BranchId { get; set; }

        [Required(ErrorMessage = "Please enter start time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Please enter end time")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Please enter room number")]
        public string Room { get; set; } = null!;

        [Required(ErrorMessage = "Please enter customer name")]
        public string CustomerName { get; set; } = null!;

        public string? CustomerPhone { get; set; }
        public string? ReceptionistName { get; set; }
        public string? Note { get; set; }

        // Ensure the list is not empty
        [MinLength(1, ErrorMessage = "Please add at least one item to the order")]
        public ICollection<ServiceItem> Items { get; set; } = null!;
    }
}
