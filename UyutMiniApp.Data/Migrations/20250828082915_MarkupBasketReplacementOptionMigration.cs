using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MarkupBasketReplacementOptionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkUp",
                table: "BasketReplacementSelections",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkUp",
                table: "BasketReplacementSelections");
        }
    }
}
