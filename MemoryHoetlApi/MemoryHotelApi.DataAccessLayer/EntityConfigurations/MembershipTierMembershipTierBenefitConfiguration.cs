using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class MembershipTierMembershipTierBenefitConfiguration : IEntityTypeConfiguration<MembershipTierMembershipTierBenefit>
    {
        public void Configure(EntityTypeBuilder<MembershipTierMembershipTierBenefit> builder)
        {
            builder.HasKey(mtmtb => new {mtmtb.MembershipTierId, mtmtb.MembershipTierBenefitId });

            builder.HasOne(mtmtb => mtmtb.MembershipTier)
                .WithMany(mt => mt.Benefits)
                .HasForeignKey(mtmtb => mtmtb.MembershipTierId);

            builder.HasOne(mtmtb => mtmtb.MembershipTierBenefit)
                .WithMany(mt => mt.Tiers)
                .HasForeignKey(mtmtb => mtmtb.MembershipTierBenefitId);
        }
    }
}
