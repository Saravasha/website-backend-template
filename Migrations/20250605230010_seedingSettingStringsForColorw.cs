using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class seedingSettingStringsForColorw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartColor",
                value: "#ffff6a");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#000000", "#ffffff" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#000000", "#ffffff" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#000000", "#ffffff" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#000000", "#ffffff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartColor",
                value: "#00ff6a");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#052e16", "#00ff6a" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#052e16", "#00ff6a" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#052e16", "#00ff6a" });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DarkStartColor", "StartColor" },
                values: new object[] { "#052e16", "#00ff6a" });
        }
    }
}
