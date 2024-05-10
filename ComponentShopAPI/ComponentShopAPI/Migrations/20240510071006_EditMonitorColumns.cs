using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditMonitorColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Monitors");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Monitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Monitors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Monitors");

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
