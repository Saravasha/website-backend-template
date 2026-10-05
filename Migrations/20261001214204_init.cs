using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opacity = table.Column<float>(type: "real", nullable: false),
                    DarkStartColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DarkEndColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DarkOpacity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    IsOrphan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Pages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fonts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fonts_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetCategory",
                columns: table => new
                {
                    AssetsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategory", x => new { x.AssetsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AssetCategory_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsId = table.Column<int>(type: "int", nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LoginImageAssetId = table.Column<int>(type: "int", nullable: true),
                    FaviconAssetId = table.Column<int>(type: "int", nullable: true),
                    HomescreenAssetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branding_Assets_FaviconAssetId",
                        column: x => x.FaviconAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Branding_Assets_HomescreenAssetId",
                        column: x => x.HomescreenAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Branding_Assets_LoginImageAssetId",
                        column: x => x.LoginImageAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Branding_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsId = table.Column<int>(type: "int", nullable: false),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramVisible = table.Column<bool>(type: "bit", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookVisible = table.Column<bool>(type: "bit", nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterVisible = table.Column<bool>(type: "bit", nullable: false),
                    TwitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedia_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "DarkEndColor", "DarkOpacity", "DarkStartColor", "EndColor", "Name", "Opacity", "StartColor" },
                values: new object[,]
                {
                    { 1, "#000000", 1f, "#000000", "#000000", "Background Color", 1f, "#000000" },
                    { 2, "#ffffff", 1f, "#ffffff", "#ffffff", "Header Text", 1f, "#ffffff" },
                    { 3, "#ffffff", 1f, "#052e16", "#ffffff", "Navbar Background Color", 1f, "#ffff6a" },
                    { 4, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-0 Page Header Text Color", 1f, "#ffffff" },
                    { 5, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-1 Chapter Header Text Color", 1f, "#ffffff" },
                    { 6, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-2 Content Header Text Color", 1f, "#ffffff" },
                    { 7, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-0 Page Body Text Color", 1f, "#ffffff" },
                    { 8, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-1 Chapter Body Text Color", 1f, "#ffffff" },
                    { 9, "#ffffff", 1f, "#000000", "#ffffff", "Page-depth-2 Content Body Text Color", 1f, "#ffffff" },
                    { 10, "#052e16", 1f, "#052e16", "#052e16", "ScrollToTop Background Color", 1f, "#052e16" },
                    { 11, "#ffffff", 1f, "#ffffff", "#000000", "Navbar Text Color", 1f, "#000000" },
                    { 12, "#15803D", 1f, "#15803D", "#15803D", "Instagram Background Icon Color", 1f, "#15803D" },
                    { 13, "#15803D", 1f, "#15803D", "#15803D", "Facebook Background Icon Color", 1f, "#15803D" },
                    { 14, "#15803D", 1f, "#15803D", "#15803D", "Twitter Background Icon Color", 1f, "#15803D" },
                    { 15, "#ffffff", 1f, "#ffffff", "#ffffff", "Instagram Fill Icon Color", 1f, "#ffffff" },
                    { 16, "#ffffff", 1f, "#ffffff", "#ffffff", "Facebook Fill Icon Color", 1f, "#ffffff" },
                    { 17, "#ffffff", 1f, "#ffffff", "#ffffff", "Twitter Fill Icon Color", 1f, "#ffffff" },
                    { 18, "#15803D", 1f, "#15803D", "#15803D", "Social Media Header Text", 1f, "#15803D" },
                    { 19, "#15803D", 1f, "#15803D", "#15803D", "Footer Text", 1f, "#15803D" }
                });

            migrationBuilder.InsertData(
                table: "Fonts",
                columns: new[] { "Id", "AssetId", "Name", "Style", "Weight" },
                values: new object[,]
                {
                    { 1, null, "Website Title Header Text Font", "normal", 100 },
                    { 2, null, "Page-depth-0 Page Header Text Font", "normal", 100 },
                    { 3, null, "Social Media Header Text", "normal", 100 },
                    { 4, null, "Website Title Footer Text Font", "normal", 100 }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Branding",
                columns: new[] { "Id", "AppName", "Description", "FaviconAssetId", "HomescreenAssetId", "LoginImageAssetId", "SettingsId" },
                values: new object[] { 1, "__PROJECT_NAME__", "__PROJECT_NAME__ - web application for managing productions, assets, and content.", null, null, null, 1 });

            migrationBuilder.InsertData(
                table: "SocialMedia",
                columns: new[] { "Id", "FacebookUrl", "FacebookVisible", "HeaderText", "InstagramUrl", "InstagramVisible", "SettingsId", "TwitterUrl", "TwitterVisible" },
                values: new object[] { 1, "https://www.facebook.com/__FACEBOOK_USERNAME__", true, "Follow me @", "https://www.instagram.com/__INSTAGRAM_USERNAME__", true, 1, "https://x.com/__TWITTER_USERNAME__", true });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategory_CategoriesId",
                table: "AssetCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Branding_FaviconAssetId",
                table: "Branding",
                column: "FaviconAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Branding_HomescreenAssetId",
                table: "Branding",
                column: "HomescreenAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Branding_LoginImageAssetId",
                table: "Branding",
                column: "LoginImageAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Branding_SettingsId",
                table: "Branding",
                column: "SettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fonts_AssetId",
                table: "Fonts",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ParentId",
                table: "Pages",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_SettingsId",
                table: "SocialMedia",
                column: "SettingsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssetCategory");

            migrationBuilder.DropTable(
                name: "Branding");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Fonts");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
