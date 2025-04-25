using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace MemoryHotelApi.DataAccessLayer.Contexts
{
    public class MemoryHotelApiDbContext(DbContextOptions<MemoryHotelApiDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images {  get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<SubTour> SubTours { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Convenience> Conveniences { get; set; }
        public DbSet<LocationExplore> LocationExplores { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<MembershipTierBenefit> MembershipTierBenefits { get; set; }
        public DbSet<MembershipTier> MembershipTiers { get; set; }
        public DbSet<MembershipTierMembershipTierBenefit> MembershipTierMembershipTierBenefits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemoryHotelApiDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow; // Use UTC for consistency

                if (entry.State == EntityState.Added)
                {
                    entity.Id = Guid.NewGuid();
                    entity.CreatedDate = now;
                    entity.IsDeleted = false;
                    entity.IsActive = true;
                }
                entity.UpdatedDate = now;
            }
        }
    }
}
