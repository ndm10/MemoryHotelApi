using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;

namespace MemoryHotelApi.DataAccessLayer.Repositories
{
    public class GroupChatZaloRepository : GenericRepository<GroupChatZalo>, IGroupChatZaloRepository
    {
        public GroupChatZaloRepository(MemoryHotelApiDbContext context) : base(context)
        {
        }
    }
}
