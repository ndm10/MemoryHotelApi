using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;
using MemoryHotelApi.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto
{
    public class RequestCarBookingDto
    {
        [Required(ErrorMessage = "Please enter pickup location code")]
        public string PickupLocation { get; set; } = null!;

        [Required(ErrorMessage = "Please enter destination code")]
        public string Destination { get; set; } = null!;

        [Required(ErrorMessage = "Please enter room number")]
        public string Room { get; set; } = null!;

        [Required(ErrorMessage = "Please enter customer name")]
        public string CustomerName { get; set; } = null!;

        public string? CustomerPhone { get; set; }
        public string? ReceptionistName { get; set; }
        public string? Note { get; set; }

        // Ensure the list is not empty
        [MinLength(1, ErrorMessage = "Please add at least one item")]
        public ICollection<ServiceItem> Items { get; set; } = null!;
    }
}
