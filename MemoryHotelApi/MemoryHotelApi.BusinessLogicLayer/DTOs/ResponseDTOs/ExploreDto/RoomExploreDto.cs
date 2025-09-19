using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class RoomExploreDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Area { get; set; }
        public string? BedType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public List<GetConvenienceCommonDto>? Conveniences { get; set; }
        public List<string>? Images { get; set; }
    }
}
