using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class SubTourConfiguration : IEntityTypeConfiguration<SubTour>
    {
        public void Configure(EntityTypeBuilder<SubTour> builder)
        {
            // SubTour - Tour relationship
            builder.HasOne(st => st.Tour)
                .WithMany(t => t.SubTours)
                .HasForeignKey(st => st.TourId);

            // SubTour - Image relationship
            builder.HasMany(st => st.Images)
                .WithMany(i => i.SubTours);
        }
    }
}
