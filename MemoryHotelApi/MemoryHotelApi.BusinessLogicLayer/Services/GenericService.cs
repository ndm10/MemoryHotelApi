using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {

        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
