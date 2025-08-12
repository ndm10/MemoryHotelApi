using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class BranchImageConfiguration : IEntityTypeConfiguration<BranchImage>
    {
        public void Configure(EntityTypeBuilder<BranchImage> builder)
        {
            builder.HasKey(bi => new { bi.BranchId, bi.ImageId });

            builder.HasOne(bi => bi.Branch)
                .WithMany(b => b.BranchImages)
                .HasForeignKey(bi => bi.BranchId);

            builder.HasOne(bi => bi.Image)
                .WithMany(i => i.BranchImages)
                .HasForeignKey(bi => bi.ImageId);
        }
    }
}
