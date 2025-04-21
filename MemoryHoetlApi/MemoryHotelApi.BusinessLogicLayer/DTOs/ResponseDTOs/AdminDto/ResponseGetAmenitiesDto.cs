using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetAmenitiesDto : GenericResponsePagination<GetAmenityDto>
    {
    }

    public class GetAmenityDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Description { get; set; }
        public int ? Order { get; set; }
        public bool IsActive { get; set; }
    }
}
