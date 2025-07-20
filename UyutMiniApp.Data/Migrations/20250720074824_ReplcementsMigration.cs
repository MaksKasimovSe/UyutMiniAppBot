using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplcementsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItemReplacementOptions_MenuItems_ReplacementMenuItemId",
                table: "SetItemReplacementOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItemReplacementOptions_MenuItems_ReplacementMenuItemId",
                table: "SetItemReplacementOptions",
                column: "ReplacementMenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItemReplacementOptions_MenuItems_ReplacementMenuItemId",
                table: "SetItemReplacementOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItemReplacementOptions_MenuItems_ReplacementMenuItemId",
                table: "SetItemReplacementOptions",
                column: "ReplacementMenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
