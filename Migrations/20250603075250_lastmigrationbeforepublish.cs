using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoshehArtWebApp.Migrations
{
    /// <inheritdoc />
    public partial class lastmigrationbeforepublish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "Container", "Date", "PageId", "Title" },
                values: new object[] { 7, "Fateme Gosheh är en regissör, målare och fotograf född 1961 i Iran, numera bosatt och verksam i Sverige.\r\n\r\n\r\n2002 ställde hon ut målningar under namnet \"En muslimsk kvinna - drömmar och mardrömmar\" på Liljevalchs vårsalong som föreställde döda och nakna kvinnor tillsammans med olika muslimska symboler, något som väckte uppmärksamhet och ilska. För målningarna polisanmäldes hon för hets mot folkgrupp av Sveriges muslimska råd. Svenska dagbladet och andra svenska tidningar som återgav hennes konst anmäldes också. Trots hot fortsatte hon därefter med konst som utmanar islam.\r\n\r\n\r\n2010 hade hennes dokumentärfilm Tusen och en natt (jävla sköna män) premiär.\r\n\r\n\r\nHennes verk har ställts ut runt om i världen, bland annat Florens, New York och Los Angeles.", null, 3, "Wikipedia Profil" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contents",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "Container", "Date", "PageId", "Title" },
                values: new object[] { 6, "Pending", null, 5, "Privacy Policy" });
        }
    }
}
