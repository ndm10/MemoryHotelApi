using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            // Tour - City relationship
            builder.HasOne(u => u.City)
                   .WithMany(c => c.Tours)
                   .HasForeignKey(u => u.CityId);

            // Tour - Image relationship
            builder.HasMany(t => t.Images)
                .WithMany(i => i.Tours);

            // Tour - SubTour relationship
            builder.HasMany(t => t.SubTours)
                   .WithOne(st => st.Tour)
                   .HasForeignKey(st => st.TourId);
        }
    }
}
