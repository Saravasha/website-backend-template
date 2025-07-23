using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class privacypolicyseedww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 7,
                column: "Container",
                value: "Fateme Gosheh är en regissör, målare och fotograf född 1961 i Iran, numera bosatt och verksam i Sverige.\n2002 ställde hon ut målningar under namnet \"En muslimsk kvinna - drömmar och mardrömmar\" på Liljevalchs vårsalong som föreställde döda och nakna kvinnor tillsammans med olika muslimska symboler, något som väckte uppmärksamhet och ilska. För målningarna polisanmäldes hon för hets mot folkgrupp av Sveriges muslimska råd. Svenska dagbladet och andra svenska tidningar som återgav hennes konst anmäldes också. Trots hot fortsatte hon därefter med konst som utmanar islam.\n2010 hade hennes dokumentärfilm Tusen och en natt (jävla sköna män) premiär.\nHennes verk har ställts ut runt om i världen, bland annat Florens, New York och Los Angeles.");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "Container",
                value: "Privacy and Cookie Policy\nEffective Date: [Insert Date]\nAt Goshehart.se, we respect your privacy and are committed to protecting your personal data.\nCookies\nOur website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.\nPersonal Data\nWe do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.\nData Security\nThe backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.\nYour Rights\nSince we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at info@goshehart.se.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 7,
                column: "Container",
                value: "Fateme Gosheh är en regissör, målare och fotograf född 1961 i Iran, numera bosatt och verksam i Sverige.\r\n\r\n\r\n2002 ställde hon ut målningar under namnet \"En muslimsk kvinna - drömmar och mardrömmar\" på Liljevalchs vårsalong som föreställde döda och nakna kvinnor tillsammans med olika muslimska symboler, något som väckte uppmärksamhet och ilska. För målningarna polisanmäldes hon för hets mot folkgrupp av Sveriges muslimska råd. Svenska dagbladet och andra svenska tidningar som återgav hennes konst anmäldes också. Trots hot fortsatte hon därefter med konst som utmanar islam.\r\n\r\n\r\n2010 hade hennes dokumentärfilm Tusen och en natt (jävla sköna män) premiär.\r\n\r\n\r\nHennes verk har ställts ut runt om i världen, bland annat Florens, New York och Los Angeles.");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "Container",
                value: "\"Privacy and Cookie Policy\\r\\n\\r\\nEffective Date: [Insert Date]\\r\\n\\r\\nAt [Your Website Name], we respect your privacy and are committed to protecting your personal data.\\r\\nCookies\\r\\n\\r\\nOur website does not use cookies to track visitors or personalize content. The only cookies used are for authentication purposes on the backend, which is accessible only to the site owner (administrator) for managing the website. These cookies are essential for secure login and session management and do not affect public visitors.\\r\\nPersonal Data\\r\\n\\r\\nWe do not collect, track, or share any personal data from visitors. No personal information is gathered through this website.\\r\\nData Security\\r\\n\\r\\nThe backend login area is secured and accessible only by the site owner. We take reasonable measures to protect any stored data related to site administration.\\r\\nYour Rights\\r\\n\\r\\nSince we do not collect personal data from visitors, there are no user data requests applicable. If you have questions or concerns about privacy, please contact us at [Your Contact Email].\"");
        }
    }
}
