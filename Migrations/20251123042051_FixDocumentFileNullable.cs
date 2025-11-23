using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentoLocal.Migrations
{
    /// <inheritdoc />
    public partial class FixDocumentFileNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "document_file",
                schema: "TalentoLocal",
                table: "Postulations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "document_file",
                schema: "TalentoLocal",
                table: "Postulations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
