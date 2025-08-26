using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MarkUpHasSetVerionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkUp",
                table: "SetItemReplacementOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasSetVerion",
                table: "MenuItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkUp",
                table: "SetItemReplacementOptions");

            migrationBuilder.DropColumn(
                name: "HasSetVerion",
                table: "MenuItems");
        }
    }
}
