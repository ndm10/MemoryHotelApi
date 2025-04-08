using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class GenericService<T> : IGenericService<T> where T : GenericEntity
    {

        protected readonly IMapper _mapper;

        public GenericService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
