using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class LocationExploreConfiguration : IEntityTypeConfiguration<LocationExplore>
    {
        public void Configure(EntityTypeBuilder<LocationExplore> builder)
        {
            // LocationExplore - Branch relationship
            builder.HasOne(le => le.Branch)
                .WithMany(br => br.LocationExplores)
                .HasForeignKey(le => le.BranchId);
        }
    }
}
