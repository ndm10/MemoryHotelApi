using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class GroupChatZaloConfiguration : IEntityTypeConfiguration<GroupChatZalo>
    {
        public void Configure(EntityTypeBuilder<GroupChatZalo> builder)
        {
            builder.HasIndex(x => x.GroupType);
        }
    }
}
