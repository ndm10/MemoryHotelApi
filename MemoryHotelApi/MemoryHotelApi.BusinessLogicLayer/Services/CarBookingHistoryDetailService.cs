using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class CarBookingHistoryDetailService : GenericService<CarBookingHistoryDetail>, ICarBookingHistoryDetailService
    {
        public CarBookingHistoryDetailService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
