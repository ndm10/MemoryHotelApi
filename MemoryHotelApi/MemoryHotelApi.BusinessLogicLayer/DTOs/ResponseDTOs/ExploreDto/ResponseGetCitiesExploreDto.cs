using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto
{
    public class ResponseGetCitiesExploreDto : GenericResponsePagination<CityExploreDto>
    {
    }

    public class CityExploreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string RegionKey { get; set; } = null!;
        public ICollection<TourExploreDto>? Tours { get; set; }
    }
}
