using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResolveConflictMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems",
                column: "IncludedItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems",
                column: "IncludedItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
