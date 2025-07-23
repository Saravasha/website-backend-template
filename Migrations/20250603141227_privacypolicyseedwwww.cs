using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class privacypolicyseedwwww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 7,
                column: "Container",
                value: "\r\n    <p><strong>Fateme Gosheh</strong> är en regissör, målare och fotograf född 1961 i Iran, numera bosatt och verksam i Sverige.</p>\r\n    <p>År 2002 ställde hon ut målningar under namnet <em>”En muslimsk kvinna – drömmar och mardrömmar”</em> på Liljevalchs vårsalong. Utställningen föreställde döda och nakna kvinnor tillsammans med olika muslimska symboler, något som väckte uppmärksamhet och ilska. För målningarna polisanmäldes hon för hets mot folkgrupp av Sveriges muslimska råd. Svenska Dagbladet och andra svenska tidningar som återgav hennes konst anmäldes också.</p>\r\n    <p>Trots hot fortsatte hon därefter med konst som utmanar islam.</p>\r\n    <p>År 2010 hade hennes dokumentärfilm <em>Tusen och en natt (jävla sköna män)</em> premiär.</p>\r\n    <p>Hennes verk har ställts ut runt om i världen, bland annat i Florens, New York och Los Angeles.</p>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 7,
                column: "Container",
                value: "Fateme Gosheh är en regissör, målare och fotograf född 1961 i Iran, numera bosatt och verksam i Sverige.\n2002 ställde hon ut målningar under namnet \"En muslimsk kvinna - drömmar och mardrömmar\" på Liljevalchs vårsalong som föreställde döda och nakna kvinnor tillsammans med olika muslimska symboler, något som väckte uppmärksamhet och ilska. För målningarna polisanmäldes hon för hets mot folkgrupp av Sveriges muslimska råd. Svenska dagbladet och andra svenska tidningar som återgav hennes konst anmäldes också. Trots hot fortsatte hon därefter med konst som utmanar islam.\n2010 hade hennes dokumentärfilm Tusen och en natt (jävla sköna män) premiär.\nHennes verk har ställts ut runt om i världen, bland annat Florens, New York och Los Angeles.");
        }
    }
}
