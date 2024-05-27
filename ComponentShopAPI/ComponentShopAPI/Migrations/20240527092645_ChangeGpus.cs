using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGpus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GraphicsCards",
                table: "GraphicsCards");

            migrationBuilder.RenameTable(
                name: "GraphicsCards",
                newName: "Gpus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gpus",
                table: "Gpus",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Gpus",
                table: "Gpus");

            migrationBuilder.RenameTable(
                name: "Gpus",
                newName: "GraphicsCards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GraphicsCards",
                table: "GraphicsCards",
                column: "Id");
        }
    }
}
