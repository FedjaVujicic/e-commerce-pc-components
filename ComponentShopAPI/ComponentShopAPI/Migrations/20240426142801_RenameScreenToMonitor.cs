using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameScreenToMonitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Screens",
                table: "Screens");

            migrationBuilder.RenameTable(
                name: "Screens",
                newName: "Monitors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Monitors",
                table: "Monitors",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Monitors",
                table: "Monitors");

            migrationBuilder.RenameTable(
                name: "Monitors",
                newName: "Screens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screens",
                table: "Screens",
                column: "Id");
        }
    }
}
