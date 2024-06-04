using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceAvailabilityWithQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Gpus");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Monitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Gpus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Gpus");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Gpus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
