using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentoLocal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TalentoLocal");

            migrationBuilder.CreateTable(
                name: "OfferCategories",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PostulationsStatus",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostulationsStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    requeriments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    years_experience = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    journey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    schedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    available_places = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contract_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    payment_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    publication_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    closing_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    featured = table.Column<bool>(type: "bit", nullable: true),
                    urgent = table.Column<bool>(type: "bit", nullable: true),
                    rating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Offers_OfferCategories_category_id",
                        column: x => x.category_id,
                        principalSchema: "TalentoLocal",
                        principalTable: "OfferCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Postulations",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    offer_id = table.Column<int>(type: "int", nullable: false),
                    document_file = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostulationStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postulations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Postulations_Offers_offer_id",
                        column: x => x.offer_id,
                        principalSchema: "TalentoLocal",
                        principalTable: "Offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postulations_PostulationsStatus_PostulationStatusId",
                        column: x => x.PostulationStatusId,
                        principalSchema: "TalentoLocal",
                        principalTable: "PostulationsStatus",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Postulations_PostulationsStatus_status_id",
                        column: x => x.status_id,
                        principalSchema: "TalentoLocal",
                        principalTable: "PostulationsStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                schema: "TalentoLocal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postulation_id = table.Column<int>(type: "int", nullable: false),
                    justification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Postulations_postulation_id",
                        column: x => x.postulation_id,
                        principalSchema: "TalentoLocal",
                        principalTable: "Postulations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_postulation_id",
                schema: "TalentoLocal",
                table: "Evaluations",
                column: "postulation_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_category_id",
                schema: "TalentoLocal",
                table: "Offers",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Postulations_offer_id",
                schema: "TalentoLocal",
                table: "Postulations",
                column: "offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Postulations_PostulationStatusId",
                schema: "TalentoLocal",
                table: "Postulations",
                column: "PostulationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulations_status_id",
                schema: "TalentoLocal",
                table: "Postulations",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations",
                schema: "TalentoLocal");

            migrationBuilder.DropTable(
                name: "Postulations",
                schema: "TalentoLocal");

            migrationBuilder.DropTable(
                name: "Offers",
                schema: "TalentoLocal");

            migrationBuilder.DropTable(
                name: "PostulationsStatus",
                schema: "TalentoLocal");

            migrationBuilder.DropTable(
                name: "OfferCategories",
                schema: "TalentoLocal");
        }
    }
}
