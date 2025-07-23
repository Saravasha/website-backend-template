using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class changedseedheadercolortowhite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DarkEndColor", "DarkStartColor", "EndColor", "StartColor" },
                values: new object[] { "#ffffff", "#ffffff", "#ffffff", "#ffffff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DarkEndColor", "DarkStartColor", "EndColor", "StartColor" },
                values: new object[] { "#000000", "#000000", "#000000", "#000000" });
        }
    }
}
