namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUpdateMembershipTierDto
    {
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
        public List<Benefit>? Benefits { get; set; }
    }
}
