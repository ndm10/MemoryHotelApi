using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace MemoryHotelApi.DataAccessLayer.Contexts
{
    public class MemoryHotelApiDbContext(DbContextOptions<MemoryHotelApiDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images { get; set; }
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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<BlogHashTag> BlogHashtags { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<SubFoodCategory> SubFoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodOrderHistory> FoodOrderHistories { get; set; }
        public DbSet<FoodOrderHistoryDetail> FoodOrderHistoryDetails { get; set; }
        public DbSet<BranchReceptionist> BranchReceptionists { get; set; }
        public DbSet<ZaloOaAuthenticationToken> ZaloOaAuthenticationTokens { get; set; }
        public DbSet<GroupChatZalo> GroupChatZalos { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MotorcycleRentalHistory> MotorcycleRentalHistories { get; set; }
        public DbSet<MotorcycleRentalHistoryDetail> MotorcycleRentalHistoryDetails { get; set; }
        public DbSet<CarBookingHistory> CarBookingHistories { get; set; }
        public DbSet<CarBookingHistoryDetail> CarBookingHistoryDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemoryHotelApiDbContext).Assembly);

            // Apply soft delete filter to all entities implementing BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var filter = Expression.Lambda(Expression.Not(property), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
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
                var now = DateTime.Now; // Use UTC for consistency

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.IsDeleted = false;
                    entity.IsActive = true;
                }
                entity.UpdatedDate = now;
            }
        }
    }
}
