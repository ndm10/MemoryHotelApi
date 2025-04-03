using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MemoryHotelApi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FullName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("b1860226-3a78-4b5e-a332-fae52b3b7e4d"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(5094), "Admin role", false, "Admin", new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(5096) },
                    { new Guid("f0263e28-97d6-48eb-9b7a-ebd9b383a7e7"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(5099), "User role", false, "User", new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(5100) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FullName", "IsDeleted", "Password", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("036aa9ee-c2a9-4225-90a3-d4fba9d6b368"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(3194), "user@1234", "User", false, "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni", "0123456789", null, null, new Guid("f0263e28-97d6-48eb-9b7a-ebd9b383a7e7"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(3195) },
                    { new Guid("4d3bc8ac-3cc5-49dc-b526-7cce24d79c5e"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(3186), "admin@1234", "Admin", false, "$2a$12$4UzizvZsV3N560sv3.VX9Otmjqx9VYCn7LzCxeZZm0s4N01/y92Ni", "0123456789", null, null, new Guid("b1860226-3a78-4b5e-a332-fae52b3b7e4d"), new DateTime(2025, 4, 3, 15, 10, 38, 56, DateTimeKind.Utc).AddTicks(3189) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
