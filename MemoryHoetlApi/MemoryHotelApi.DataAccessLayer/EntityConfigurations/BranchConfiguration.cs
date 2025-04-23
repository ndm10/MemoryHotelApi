using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MemoryHotelApi.DataAccessLayer.EntityConfigurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Branch - Image relationship
            builder.HasMany(br => br.Images)
                .WithMany(img => img.Branches);

            // Convenience - Branch relationship
            builder.HasMany(br => br.GeneralConveniences)
                .WithMany(a => a.BranchesWithGeneralConvenience)
                .UsingEntity(x => x.ToTable("BranchGeneralConveniences"));

            builder.HasMany(br => br.HighlightedConveniences)
                .WithMany(a => a.BranchesWithHighlightedConvenience)
                .UsingEntity(x => x.ToTable("BranchHighlightedConveniences"));

            builder.HasIndex(b => b.Slug)
                .IsUnique();

            // Configure the properties for JSON serialization and deserialization
            builder.Property(b => b.LocationHighlights)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, _jsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<string>>(v, _jsonSerializerOptions) ?? new List<string>())
                .HasColumnType("json");
            builder.Property(b => b.SuitableFor)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, _jsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<string>>(v, _jsonSerializerOptions) ?? new List<string>())
                .HasColumnType("json");
        }
    }
}
