using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemoryHotelApi.DataAccessLayer.SeedData
{
    public class DataSeeder
    {
        private readonly MemoryHotelApiDbContext _context;

        public DataSeeder(MemoryHotelApiDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            await _context.Database.MigrateAsync();

            if (!await _context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Id = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                        Name = "Admin",
                        Description = "Admin role",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        IsActive = true
                    },
                    new Role
                    {
                        Id = Guid.Parse("f0263e28-97d6-48eb-9b7a-ebd9b383a7e7"),
                        Name = "User",
                        Description = "User role",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        IsActive = true
                    }
                };
                _context.Roles.AddRange(roles);
            }

            if (!await _context.Users.AnyAsync())
            {
                var users = new List<User>
                {
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
                        FullName = "User",
                        IsVerified = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                        Email = "ducyb782001@gmail.com",
                        Phone = "0123456789",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        RoleId = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                        FullName = "Nguyễn Đình Trung Đức",
                        IsVerified = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                        Email = "ndminh1010@gmail.com",
                        Phone = "0123456789",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        RoleId = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                        FullName = "Minh Nguyễn",
                        IsVerified = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                        Email = "quangyb1234@gmail.com",
                        Phone = "0123456789",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        RoleId = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                        FullName = "Nguyễn Ngọc Quang",
                        IsVerified = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Password = "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni",
                        Email = "admin@gmail.com",
                        Phone = "0123456789",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        RoleId = Guid.Parse("b1860226-3a78-4b5e-a332-fae52b3b7e4d"),
                        FullName = "Admin",
                        IsVerified = true,
                        IsActive = true,
                    }
                };
                _context.Users.AddRange(users);
            }

            // Seed data for city
            if (!await _context.Cities.AnyAsync())
            {
                var cities = new List<City>
                {
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hà Nội",
                        Region = "Miền Bắc",
                        Order = 1,
                        RegionKey = "north",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        IsActive = true
                    },
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "Đà Nẵng",
                        Region = "Miền Trung",
                        Order = 2,
                        RegionKey = "central",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        IsActive = true
                    },
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hồ Chí Minh",
                        Region = "Miền Nam",
                        Order = 3,
                        RegionKey = "south",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        IsActive = true
                    }
                };
                _context.Cities.AddRange(cities);
            }

            await _context.SaveChangesAsync();
        }
    }
}
