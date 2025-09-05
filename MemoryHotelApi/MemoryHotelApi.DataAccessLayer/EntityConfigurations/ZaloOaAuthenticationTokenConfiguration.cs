using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class ZaloOaAuthenticationTokenConfiguration : IEntityTypeConfiguration<ZaloOaAuthenticationToken>
    {
        public void Configure(EntityTypeBuilder<ZaloOaAuthenticationToken> builder)
        {
            builder.HasIndex(x => x.IsUsed);
        }
    }
}
