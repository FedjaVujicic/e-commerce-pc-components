using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGpu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Memory",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ports",
                table: "Components",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slot",
                table: "Components",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Memory",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Ports",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Slot",
                table: "Components");
        }
    }
}
