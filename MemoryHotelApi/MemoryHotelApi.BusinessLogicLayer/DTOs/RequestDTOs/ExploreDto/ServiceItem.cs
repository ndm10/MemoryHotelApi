using MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto
{
    public class ServiceItem
    {
        public Guid Id { get; set; }

        [NumberGreaterThan(0, ErrorMessage = "Please enter quantity greater than 0")]
        public int Quantity { get; set; }
    }
}
