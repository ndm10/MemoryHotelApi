using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ThomVietApi.DataAccessLayer.SeedData
{
    public class RoleSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                    Name = "Admin",
                    Description = "Admin role",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Role
                {
                    Id = Guid.Parse("f0263e28-97d6-48eb-9b7a-ebd9b383a7e7"),
                    Name = "User",
                    Description = "User role",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            );
        }
    }
}
