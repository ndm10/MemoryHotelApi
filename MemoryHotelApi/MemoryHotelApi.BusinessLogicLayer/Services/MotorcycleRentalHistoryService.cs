using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class MotorcycleRentalHistoryService : GenericService<MotorcycleRentalHistory>, IMotorcycleRentalHistoryService
    {
        public MotorcycleRentalHistoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
