using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Ports",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Slot",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "Components",
                newName: "Screens");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Screens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RefreshRate",
                table: "Screens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screens",
                table: "Screens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    Ports = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GraphicsCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screens",
                table: "Screens");

            migrationBuilder.RenameTable(
                name: "Screens",
                newName: "Components");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Components",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RefreshRate",
                table: "Components",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Components",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                table: "Components",
                column: "Id");
        }
    }
}
