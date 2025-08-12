using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class ConvenienceConfiguration : IEntityTypeConfiguration<Convenience>
    {
        public void Configure(EntityTypeBuilder<Convenience> builder)
        {
            // Convenience - Branch relationship
            builder.HasMany(a => a.BranchesWithGeneralConvenience)
                .WithMany(b => b.GeneralConveniences);
            builder.HasMany(a => a.BranchesWithHighlightedConvenience)
                .WithMany(b => b.HighlightedConveniences);
        }
    }
}
