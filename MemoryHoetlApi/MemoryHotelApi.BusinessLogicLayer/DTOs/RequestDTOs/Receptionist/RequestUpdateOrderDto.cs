using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist
{
    public class RequestUpdateOrderDto
    {
        [Required(ErrorMessage = "Please select the status for the order")]
        public string Status { get; set; } = null!;
    }
}
