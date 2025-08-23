using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryHotelApi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Update_FoodOrderHistory_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerPhone",
                table: "FoodOrderHistories",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "FoodOrderHistories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "FoodOrderHistories");

            migrationBuilder.UpdateData(
                table: "FoodOrderHistories",
                keyColumn: "CustomerPhone",
                keyValue: null,
                column: "CustomerPhone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPhone",
                table: "FoodOrderHistories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
