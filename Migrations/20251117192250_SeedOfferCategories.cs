using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TalentoLocal.Migrations
{
    /// <inheritdoc />
    public partial class SeedOfferCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                columns: new[] { "id", "create_at", "name", "update_at" },
                values: new object[,]
                {
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tecnología", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administración", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salud", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Educación", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing y Comunicación", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ingeniería", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencias Ambientales", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turismo y Hotelería", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logística y Operaciones", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arte y Cultura", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 12);
        }
    }
}
