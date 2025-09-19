using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class MotorcycleRentalHistoryDetailService : GenericService<MotorcycleRentalHistoryDetail>, IMotorcycleRentalHistoryDetailService
    {
        public MotorcycleRentalHistoryDetailService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
