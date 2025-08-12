using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class SubTourRepository : GenericRepository<SubTour>, ISubTourRepository
    {
        public SubTourRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
