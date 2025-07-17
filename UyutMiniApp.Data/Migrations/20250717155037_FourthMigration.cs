using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItems_MenuItems_MenuItemId",
                table: "SetItems");

            migrationBuilder.AddColumn<Guid>(
                name: "IncludedItemId",
                table: "SetItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SetItems_IncludedItemId",
                table: "SetItems",
                column: "IncludedItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems",
                column: "IncludedItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SetItems_MenuItems_MenuItemId",
                table: "SetItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetItems_MenuItems_IncludedItemId",
                table: "SetItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SetItems_MenuItems_MenuItemId",
                table: "SetItems");

            migrationBuilder.DropIndex(
                name: "IX_SetItems_IncludedItemId",
                table: "SetItems");

            migrationBuilder.DropColumn(
                name: "IncludedItemId",
                table: "SetItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SetItems_MenuItems_MenuItemId",
                table: "SetItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
