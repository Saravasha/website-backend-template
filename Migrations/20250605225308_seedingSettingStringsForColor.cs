using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class seedingSettingStringsForColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "DarkEndColor", "DarkStartColor", "EndColor", "Name", "StartColor" },
                values: new object[,]
                {
                    { 3, "#ffffff", "#052e16", "#ffffff", "Navbar Background Color", "#00ff6a" },
                    { 4, "#ffffff", "#052e16", "#ffffff", "Page Header Text Color", "#00ff6a" },
                    { 5, "#ffffff", "#052e16", "#ffffff", "Content Header Text Color", "#00ff6a" },
                    { 6, "#ffffff", "#052e16", "#ffffff", "Page Body Text Color", "#00ff6a" },
                    { 7, "#ffffff", "#052e16", "#ffffff", "Content Body Text Color", "#00ff6a" },
                    { 8, "#052e16", "#052e16", "#052e16", "ScrollToTop Background Color", "#052e16" },
                    { 9, "#ffffff", "#ffffff", "#000000", "Navbar Text Color", "#000000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
