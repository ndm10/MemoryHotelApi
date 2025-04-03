using Microsoft.EntityFrameworkCore;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace ThomVietApi.DataAccessLayer.SeedData
{
    public static class UserSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed data for User table
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("4d3bc8ac-3cc5-49dc-b526-7cce24d79c5e"),
                    Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                    Email = "admin@1234",
                    Phone = "0123456789",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    RoleId = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                    FullName = "Admin"
                },
                new User
                {
                    Id = Guid.Parse("036aa9ee-c2a9-4225-90a3-d4fba9d6b368"),
                    Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                    Email = "user@1234",
                    Phone = "0123456789",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    RoleId = Guid.Parse("f0263e28-97d6-48eb-9b7a-ebd9b383a7e7"),
                    FullName = "User"
                }
            );

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
