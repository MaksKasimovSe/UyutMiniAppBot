using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BasketItemQuantityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MenuItemsBaskets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MenuItemsBaskets");
        }
    }
}
