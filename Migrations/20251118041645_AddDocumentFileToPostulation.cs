using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentoLocal.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentFileToPostulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "document_file",
                schema: "TalentoLocal",
                table: "Postulations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 3,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 4,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 5,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 6,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 7,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 8,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 9,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 10,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 11,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 12,
                column: "create_at",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "document_file",
                schema: "TalentoLocal",
                table: "Postulations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 3,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 4,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 5,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 6,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 7,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 8,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 9,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 10,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 11,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "TalentoLocal",
                table: "OfferCategories",
                keyColumn: "id",
                keyValue: 12,
                column: "create_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
