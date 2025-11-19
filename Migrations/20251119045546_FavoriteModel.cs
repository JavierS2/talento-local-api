using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentoLocal.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    offer_id = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.id);
                    table.ForeignKey(
                        name: "FK_Favorites_Offers_offer_id",
                        column: x => x.offer_id,
                        principalSchema: "TalentoLocal",
                        principalTable: "Offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_offer_id",
                schema: "TalentoLocal",
                table: "Favorites",
                column: "offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_user_id_offer_id",
                schema: "TalentoLocal",
                table: "Favorites",
                columns: new[] { "user_id", "offer_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites",
                schema: "TalentoLocal");
        }
    }
}
