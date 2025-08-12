using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class BlogHashTagConfiguration : IEntityTypeConfiguration<BlogHashTag>
    {
        public void Configure(EntityTypeBuilder<BlogHashTag> builder)
        {
            builder.HasKey(bht => new { bht.BlogId, bht.HashtagId });

            builder.HasOne(bht => bht.Hashtag)
                .WithMany(h => h.BlogHashtags)
                .HasForeignKey(bht => bht.HashtagId);

            builder.HasOne(bht => bht.Blog)
                .WithMany(b => b.BlogHashtags)
                .HasForeignKey(bht => bht.BlogId);
        }
    }
}