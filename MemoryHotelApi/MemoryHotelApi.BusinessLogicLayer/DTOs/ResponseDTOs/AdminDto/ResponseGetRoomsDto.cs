using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetRoomsDto : GenericResponsePaginationDto<RoomDto>
    {
    }

    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Area { get; set; }
        public string BedType { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public List<ConvenienceDto>? Conveniences { get; set; }
        public List<string>? Images { get; set; }
        public RoomBranchDto Branch { get; set; } = null!;
        public RoomCategoryDto RoomCategory { get; set; } = null!;
    }

    public class RoomBranchDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
