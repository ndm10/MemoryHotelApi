namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class RoomCategoryExploreDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Order { get; set; }
        public List<RoomExploreDto>? Rooms { get; set; }
    }
}
