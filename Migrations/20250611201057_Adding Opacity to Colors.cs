using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingOpacitytoColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DarkOpacity",
                table: "Colors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Opacity",
                table: "Colors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DarkOpacity", "Opacity" },
                values: new object[] { 1f, 1f });

            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 6,
                column: "Container",
                value: "\r\n            <h2>Privacy and Cookie Policy</h2>\r\n            <p>At Goshehart.se, we respect your privacy and are committed to protecting your personal data.</p>\r\n            <h3>Cookies</h3>\r\n            <p>Our website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.</p>\r\n            <h3>Personal Data</h3>\r\n            <p>We do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.</p>\r\n            <h3>Data Security</h3>\r\n            <p>The backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.</p>\r\n            <h3>Your Rights</h3>\r\n            <p>Since we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at <a href='mailto:info@goshehart.se'>info@goshehart.se</a></p>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkOpacity",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Opacity",
                table: "Colors");

            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 6,
                column: "Container",
                value: "\r\n            <h2>Privacy and Cookie Policy</h2>\r\n            <p>At Goshehart.se, we respect your privacy and are committed to protecting your personal data.</p>\r\n            <h3>Cookies</h3>\r\n            <p>Our website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.</p>\r\n            <h3>Personal Data</h3>\r\n            <p>We do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.</p>\r\n            <h3>Data Security</h3>\r\n            <p>The backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.</p>\r\n            <h3>Your Rights</h3>\r\n            <p>Since we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at <a href='mailto:info@goshehart.se'>info@goshehart.se</a>.</p>");
        }
    }
}
