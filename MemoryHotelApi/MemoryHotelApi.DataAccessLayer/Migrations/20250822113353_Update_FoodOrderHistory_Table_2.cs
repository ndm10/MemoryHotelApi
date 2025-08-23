using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryHotelApi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Update_FoodOrderHistory_Table_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceptionistName",
                table: "FoodOrderHistories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceptionistName",
                table: "FoodOrderHistories");
        }
    }
}
