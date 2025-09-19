namespace MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs
{
    public class GetConvenienceCommonDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
