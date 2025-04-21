using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Branch - Image relationship
            builder.HasMany(br => br.Images)
                .WithMany(img => img.Branches);

            // Amenity - Branch relationship
            builder.HasMany(br => br.Amenities)
                .WithMany(a => a.Branches);
        }
    }
}
