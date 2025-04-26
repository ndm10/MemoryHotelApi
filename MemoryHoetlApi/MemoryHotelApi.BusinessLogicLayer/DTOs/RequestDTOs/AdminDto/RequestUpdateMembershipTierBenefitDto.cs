namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateMembershipTierBenefitDto
    {
        public string? Benefit { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int? Order { get; set; }
    }
}
