using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class privacypolicyseedw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "Container",
                value: "\"Privacy and Cookie Policy\\r\\n\\r\\nEffective Date: [Insert Date]\\r\\n\\r\\nAt [Your Website Name], we respect your privacy and are committed to protecting your personal data.\\r\\nCookies\\r\\n\\r\\nOur website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.\\r\\nPersonal Data\\r\\n\\r\\nWe do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.\\r\\nData Security\\r\\n\\r\\nThe backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.\\r\\nYour Rights\\r\\n\\r\\nSince we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at [Your Contact Email].\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "Container", "Date", "PageId", "Title" },
                values: new object[] { 6, "Privacy and Cookie Policy\r\n\r\nEffective Date: [Insert Date]\r\n\r\nAt [Your Website Name], we respect your privacy and are committed to protecting your personal data.\r\nCookies\r\n\r\nOur website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.\r\nPersonal Data\r\n\r\nWe do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.\r\nData Security\r\n\r\nThe backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.\r\nYour Rights\r\n\r\nSince we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at [Your Contact Email].", null, 5, "Privacy Policy" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "Container",
                value: "Privacy");
        }
    }
}
