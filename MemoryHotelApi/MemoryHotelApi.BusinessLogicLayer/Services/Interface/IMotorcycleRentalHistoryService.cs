using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IMotorcycleRentalHistoryService : IGenericService<MotorcycleRentalHistory>
    {
        Task<BaseResponseDto> RequestCreateMotorcycleRentalAsync(RequestMotorcycleRentalDto request);
    }
}
