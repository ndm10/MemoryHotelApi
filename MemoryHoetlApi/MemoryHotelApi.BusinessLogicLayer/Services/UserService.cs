using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IMapper mapper) : base(mapper)
        {
        }
    }
}
