using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UyutMiniApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BasketRelationsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasketReplacementSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuItemBasketId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalSetItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplacementMenuItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketReplacementSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketReplacementSelections_MenuItemsBaskets_MenuItemBasket~",
                        column: x => x.MenuItemBasketId,
                        principalTable: "MenuItemsBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketReplacementSelections_MenuItems_ReplacementMenuItemId",
                        column: x => x.ReplacementMenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketReplacementSelections_SetItems_OriginalSetItemId",
                        column: x => x.OriginalSetItemId,
                        principalTable: "SetItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketTopings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuItemBasketId = table.Column<Guid>(type: "uuid", nullable: false),
                    TopingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketTopings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketTopings_MenuItemsBaskets_MenuItemBasketId",
                        column: x => x.MenuItemBasketId,
                        principalTable: "MenuItemsBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketTopings_Topings_TopingId",
                        column: x => x.TopingId,
                        principalTable: "Topings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketReplacementSelections_MenuItemBasketId",
                table: "BasketReplacementSelections",
                column: "MenuItemBasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketReplacementSelections_OriginalSetItemId",
                table: "BasketReplacementSelections",
                column: "OriginalSetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketReplacementSelections_ReplacementMenuItemId",
                table: "BasketReplacementSelections",
                column: "ReplacementMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketTopings_MenuItemBasketId",
                table: "BasketTopings",
                column: "MenuItemBasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketTopings_TopingId",
                table: "BasketTopings",
                column: "TopingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketReplacementSelections");

            migrationBuilder.DropTable(
                name: "BasketTopings");
        }
    }
}
