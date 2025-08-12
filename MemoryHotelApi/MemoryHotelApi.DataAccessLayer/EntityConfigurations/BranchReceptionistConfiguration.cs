using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class BranchReceptionistConfiguration : IEntityTypeConfiguration<BranchReceptionist>
    {
        public void Configure(EntityTypeBuilder<BranchReceptionist> builder)
        {
            builder.HasKey(br => new { br.BranchId, br.UserId });

            builder.HasOne(br => br.Branch)
                .WithMany(b => b.BranchReceptionists)
                .HasForeignKey(br => br.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(br => br.User)
                .WithMany(u => u.BranchReceptionists)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
