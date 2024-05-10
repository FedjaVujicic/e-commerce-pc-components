using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefreshRate",
                table: "Components",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshRate",
                table: "Components");
        }
    }
}
