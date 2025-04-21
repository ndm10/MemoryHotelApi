using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            // Amenity - Branch relationship
            builder.HasMany(a => a.Branches)
                .WithMany(b => b.Amenities);
        }
    }
}
