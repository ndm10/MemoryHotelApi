using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class HashtagRepository : GenericRepository<Hashtag>, IHashtagRepository
    {
        public HashtagRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
