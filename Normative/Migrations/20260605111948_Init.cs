using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Normative.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cfg");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "cfg",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "PreparationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLine",
                columns: table => new
                {
                    ProductLine_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(63)", unicode: false, maxLength: 63, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VyrobekR__3214EC276286AE61", x => x.ProductLine_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSize",
                columns: table => new
                {
                    ProductSize_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(63)", unicode: false, maxLength: 63, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VyrobekV__3214EC27014D033A", x => x.ProductSize_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductType_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(63)", unicode: false, maxLength: 63, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VyrobekT__3214EC27C0ED5D98", x => x.ProductType_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "cfg",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "cfg",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Preparations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSizeId = table.Column<int>(type: "int", nullable: false),
                    PreparationTypeId = table.Column<int>(type: "int", nullable: false),
                    Fitter = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Welder = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preparations_PreparationType_PreparationTypeId",
                        column: x => x.PreparationTypeId,
                        principalTable: "PreparationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Preparations_ProductSize_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSize",
                        principalColumn: "ProductSize_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Operation_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductLine_Id = table.Column<int>(type: "int", nullable: true),
                    ProductType_Id = table.Column<int>(type: "int", nullable: true),
                    OperationNumber = table.Column<int>(type: "int", nullable: true),
                    OperationDescription = table.Column<string>(type: "varchar(63)", unicode: false, maxLength: 63, nullable: true),
                    WorkCenter = table.Column<string>(type: "varchar(31)", unicode: false, maxLength: 31, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Operace__3214EC271F9E5207", x => x.Operation_Id);
                    table.ForeignKey(
                        name: "FK_Operation_ProductLine",
                        column: x => x.ProductLine_Id,
                        principalTable: "ProductLine",
                        principalColumn: "ProductLine_Id");
                    table.ForeignKey(
                        name: "FK_Operation_ProductType",
                        column: x => x.ProductType_Id,
                        principalTable: "ProductType",
                        principalColumn: "ProductType_Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "cfg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "cfg",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cfg",
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "cfg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cfg",
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "cfg",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OperationStep",
                columns: table => new
                {
                    OperationStep_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation_Id = table.Column<int>(type: "int", nullable: true),
                    ProductSize_Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DrawingPosition = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    StandardHour = table.Column<int>(type: "int", nullable: true),
                    Diameter = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PipeBending = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationStep", x => x.OperationStep_Id);
                    table.ForeignKey(
                        name: "FK_OperationStep_Operation",
                        column: x => x.Operation_Id,
                        principalTable: "Operation",
                        principalColumn: "Operation_Id");
                    table.ForeignKey(
                        name: "FK_OperationStep_ProductSize",
                        column: x => x.ProductSize_Id,
                        principalTable: "ProductSize",
                        principalColumn: "ProductSize_Id");
                });

            migrationBuilder.InsertData(
                table: "PreparationType",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Příprava materiálu" },
                    { 2, false, "Příprava svarových ploch" },
                    { 3, false, "Příprava povrchu" },
                    { 4, false, "Příprava přírub" }
                });

            migrationBuilder.InsertData(
                table: "ProductLine",
                columns: new[] { "ProductLine_Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Potrubí" },
                    { 2, false, "Nádoby" },
                    { 3, false, "Výměníky tepla" }
                });

            migrationBuilder.InsertData(
                table: "ProductSize",
                columns: new[] { "ProductSize_Id", "IsDeleted", "Name", "Size" },
                values: new object[,]
                {
                    { 1, false, "DN 50", "DN50  / PN16" },
                    { 2, false, "DN 100", "DN100 / PN16" },
                    { 3, false, "DN 200", "DN200 / PN25" },
                    { 4, false, "DN 300", "DN300 / PN25" },
                    { 5, false, "DN 500", "DN500 / PN40" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductType_Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Standardní" },
                    { 2, false, "Tlakový" },
                    { 3, false, "Nerezový" }
                });

            migrationBuilder.InsertData(
                schema: "cfg",
                table: "Roles",
                columns: new[] { "RoleId", "Created", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vladce vseho", true, "Administrator" },
                    { 2, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peasant", true, "Technologie" }
                });

            migrationBuilder.InsertData(
                schema: "cfg",
                table: "Users",
                columns: new[] { "UserId", "Created", "DisplayName", "Email", "IsActive", "PasswordHash", "UserName" },
                values: new object[] { 1, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petr Docekal", "peta.docekal@gmail.com", true, "$2a$11$hHUW/3Rj/1lYTjQOC3K2B.2Nvt7yQgaSTOBJ358FkrOpb2CXPPM8.", "peta_3" });

            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Operation_Id", "IsDeleted", "OperationDescription", "OperationNumber", "ProductLine_Id", "ProductType_Id", "WorkCenter" },
                values: new object[,]
                {
                    { 1, false, "Řezání a tvarování potrubí", 10, 1, 1, "WC-10" },
                    { 2, false, "Svařování potrubí", 20, 1, 1, "WC-20" },
                    { 3, false, "Řezání tlakového potrubí", 10, 1, 2, "WC-10" },
                    { 4, false, "Svařování tlakového potrubí", 20, 1, 2, "WC-20" },
                    { 5, false, "Tlaková zkouška", 30, 1, 2, "WC-30" },
                    { 6, false, "Příprava pláště nádoby", 10, 2, 2, "WC-10" },
                    { 7, false, "Svařování dna a víka", 20, 2, 2, "WC-20" },
                    { 8, false, "Kontrola a certifikace", 40, 2, 2, "WC-40" },
                    { 9, false, "Příprava nerezového pláště", 10, 2, 3, "WC-10" },
                    { 10, false, "TIG svařování nerezových dílů", 20, 2, 3, "WC-20" },
                    { 11, false, "Příprava trubkových svazků", 10, 3, 2, "WC-10" },
                    { 12, false, "Montáž trubkového svazku do pláště", 20, 3, 2, "WC-20" }
                });

            migrationBuilder.InsertData(
                table: "Preparations",
                columns: new[] { "Id", "Fitter", "IsDeleted", "PreparationTypeId", "ProductSizeId", "Welder" },
                values: new object[,]
                {
                    { 1, 0.25m, false, 1, 1, 0.00m },
                    { 2, 0.50m, false, 1, 2, 0.00m },
                    { 3, 1.00m, false, 1, 3, 0.00m },
                    { 4, 1.50m, false, 1, 4, 0.00m },
                    { 5, 2.50m, false, 1, 5, 0.00m },
                    { 6, 0.25m, false, 2, 1, 0.25m },
                    { 7, 0.50m, false, 2, 2, 0.50m },
                    { 8, 0.75m, false, 2, 3, 0.75m },
                    { 9, 1.00m, false, 2, 4, 1.00m },
                    { 10, 1.50m, false, 2, 5, 1.50m },
                    { 11, 0.50m, false, 3, 1, 0.00m },
                    { 12, 0.75m, false, 3, 2, 0.00m },
                    { 13, 1.25m, false, 3, 3, 0.00m },
                    { 14, 2.00m, false, 3, 4, 0.00m },
                    { 15, 3.00m, false, 3, 5, 0.00m },
                    { 16, 0.50m, false, 4, 1, 0.50m },
                    { 17, 0.75m, false, 4, 2, 0.75m },
                    { 18, 1.25m, false, 4, 3, 1.25m },
                    { 19, 2.00m, false, 4, 4, 2.00m },
                    { 20, 3.50m, false, 4, 5, 3.50m }
                });

            migrationBuilder.InsertData(
                schema: "cfg",
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "OperationStep",
                columns: new[] { "OperationStep_Id", "Description", "Diameter", "DrawingPosition", "IsDeleted", "Name", "Operation_Id", "PipeBending", "ProductSize_Id", "Sequence", "StandardHour" },
                values: new object[,]
                {
                    { 1, null, "DN50", "P-01", false, "Odměření a označení", 1, null, 1, 1, 5 },
                    { 2, null, "DN50", "P-02", false, "Řezání pásovou pilou", 1, null, 1, 2, 10 },
                    { 3, null, "DN100", "P-01", false, "Odměření a označení", 1, null, 2, 1, 7 },
                    { 4, null, "DN100", "P-02", false, "Řezání pásovou pilou", 1, null, 2, 2, 15 },
                    { 5, null, "DN200", "P-01", false, "Odměření a označení", 1, null, 3, 1, 10 },
                    { 6, null, "DN200", "P-02", false, "Plasma řezání", 1, null, 3, 2, 25 },
                    { 7, null, "DN50", "S-01", false, "Stehování", 2, null, 1, 1, 15 },
                    { 8, null, "DN50", "S-02", false, "Svařování MIG", 2, null, 1, 2, 30 },
                    { 9, null, "DN100", "S-01", false, "Stehování", 2, null, 2, 1, 20 },
                    { 10, null, "DN100", "S-02", false, "Svařování MIG", 2, null, 2, 2, 50 },
                    { 11, null, "DN200", "S-01", false, "Stehování", 2, null, 3, 1, 30 },
                    { 12, null, "DN200", "S-02", false, "Svařování MIG", 2, null, 3, 2, 80 },
                    { 13, null, "DN100", "T-01", false, "Kontrola materiálu", 3, null, 2, 1, 5 },
                    { 14, null, "DN100", "T-02", false, "Řezání + zkosení", 3, null, 2, 2, 20 },
                    { 15, null, "DN300", "T-01", false, "Kontrola materiálu", 3, null, 4, 1, 8 },
                    { 16, null, "DN300", "T-02", false, "Plasma řezání + zkosení", 3, null, 4, 2, 35 },
                    { 17, null, "DN100", "S-01", false, "Stehování", 4, null, 2, 1, 25 },
                    { 18, null, "DN100", "S-02", false, "Kořenový svar TIG", 4, null, 2, 2, 40 },
                    { 19, null, "DN100", "S-03", false, "Výplňové vrstvy MIG", 4, null, 2, 3, 60 },
                    { 20, null, "DN300", "S-01", false, "Stehování", 4, null, 4, 1, 40 },
                    { 21, null, "DN300", "S-02", false, "Kořenový svar TIG", 4, null, 4, 2, 70 },
                    { 22, null, "DN300", "S-03", false, "Výplňové vrstvy MIG", 4, null, 4, 3, 110 },
                    { 23, null, "DN100", "Z-01", false, "Instalace přístrojů", 5, null, 2, 1, 15 },
                    { 24, null, "DN100", "Z-02", false, "Tlaková zkouška 1.5x PN", 5, null, 2, 2, 30 },
                    { 25, null, "DN300", "Z-01", false, "Instalace přístrojů", 5, null, 4, 1, 20 },
                    { 26, null, "DN300", "Z-02", false, "Tlaková zkouška 1.5x PN", 5, null, 4, 2, 45 },
                    { 27, null, "DN200", "N-01", false, "Válcování plechu", 6, null, 3, 1, 60 },
                    { 28, null, "DN200", "N-02", false, "Podélný svar pláště", 6, null, 3, 2, 90 },
                    { 29, null, "DN500", "N-01", false, "Válcování plechu", 6, null, 5, 1, 120 },
                    { 30, null, "DN500", "N-02", false, "Podélný svar pláště", 6, null, 5, 2, 180 },
                    { 31, null, "DN200", "D-01", false, "Přivaření eliptického dna", 7, null, 3, 1, 80 },
                    { 32, null, "DN200", "D-02", false, "Přivaření hrdel", 7, null, 3, 2, 50 },
                    { 33, null, "DN500", "D-01", false, "Přivaření eliptického dna", 7, null, 5, 1, 150 },
                    { 34, null, "DN500", "D-02", false, "Přivaření hrdel", 7, null, 5, 2, 100 },
                    { 35, null, "DN200", "K-01", false, "RT / UT kontrola svarů", 8, null, 3, 1, 60 },
                    { 36, null, "DN200", "K-02", false, "Tlaková zkouška nádoby", 8, null, 3, 2, 90 },
                    { 37, null, "DN500", "K-01", false, "RT / UT kontrola svarů", 8, null, 5, 1, 120 },
                    { 38, null, "DN500", "K-02", false, "Tlaková zkouška nádoby", 8, null, 5, 2, 180 },
                    { 39, null, "DN200", "NR-01", false, "Přesné řezání nerez", 9, null, 3, 1, 45 },
                    { 40, null, "DN200", "NR-02", false, "Moření a pasivace", 9, null, 3, 2, 30 },
                    { 41, null, "DN200", "NR-03", false, "TIG kořenový svar", 10, null, 3, 1, 60 },
                    { 42, null, "DN200", "NR-04", false, "TIG výplňové vrstvy", 10, null, 3, 2, 90 },
                    { 43, null, "DN200", "NR-05", false, "Vizuální + rozměrová kontrola", 10, null, 3, 3, 20 },
                    { 44, null, "DN50", "VS-01", false, "Řezání trubek na délku", 11, null, 1, 1, 30 },
                    { 45, null, "DN50", "VS-02", false, "Rozvrtání trubkovnice", 11, null, 1, 2, 60 },
                    { 46, null, "DN50", "VS-03", false, "Zaválcování trubek", 11, null, 1, 3, 90 },
                    { 47, null, "DN200", "VT-01", false, "Zasunutí svazku do pláště", 12, null, 3, 1, 45 },
                    { 48, null, "DN200", "VT-02", false, "Přivaření trubkovnice", 12, null, 3, 2, 120 },
                    { 49, null, "DN200", "VT-03", false, "Montáž přírub a hrdel", 12, null, 3, 3, 60 },
                    { 50, null, "DN200", "VT-04", false, "Těsnostní zkouška výměníku", 12, null, 3, 4, 90 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operation_ProductLine_Id",
                table: "Operation",
                column: "ProductLine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_ProductType_Id",
                table: "Operation",
                column: "ProductType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OperationStep_Operation_Id",
                table: "OperationStep",
                column: "Operation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OperationStep_ProductSize_Id",
                table: "OperationStep",
                column: "ProductSize_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Preparations_PreparationTypeId",
                table: "Preparations",
                column: "PreparationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Preparations_ProductSizeId",
                table: "Preparations",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "cfg",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "cfg",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "cfg",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "cfg",
                table: "UserRoles",
                column: "UserId");


            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[V_Preparation]
                AS
                SELECT        
                    p.Id, 
                    s.Name as DiameterLine,
                    s.Size, 
                    t.Name AS TypeName, 
                    CAST(p.Fitter as DECIMAL(18, 4)) as Fitter, 
                    p.Welder 
                FROM dbo.Preparations p
                    INNER JOIN dbo.ProductSize as s ON p.ProductSizeId = s.ProductSize_Id 
                    INNER JOIN dbo.PreparationType as t ON p.PreparationTypeId = t.Id
                WHERE p.IsDeleted = 0 OR s.IsDeleted = 0 OR t.IsDeleted = 0
            ");
            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_InnerVessel_OPSQ_10] AS
                SELECT
                    ok1.Sequence as Sequence,
                    vr.Name AS ProductLine,
                    vt.Name AS ProductType, 
                    ok1.Name as NameOperationStep,
                    ok1.Operation_Id as Operation_Id,
                    ok1.ProductSize_Id as ProductSize_Id,
                    ok1.Description as Note,
                    ok1.StandardHour as D1350,
                    ok2.StandardHour as D1700,
                    ok3.StandardHour as D2100,
                    ok4.StandardHour as D2500,
                    ok1.Description as 'Option'
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.OperationStep_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.Operation_Id
                    AND ok2.ProductSize_Id=6
                    AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.Operation_Id 
                    AND ok3.ProductSize_Id=7
                    AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.Operation_Id 
                    AND ok4.ProductSize_Id=3
                    AND ok1.Sequence=ok4.Sequence
                WHERE 
                    o.OperationNumber = 10 
                    AND vr.ProductLine_Id = 1 
                    AND vt.ProductType_Id = 2
                    AND ok1.ProductSize_Id = 5
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW  [dbo].[v_VTC_InnerVessel_OPSQ_20] AS
                SELECT
	                ok1.Sequence as Sequence,
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Name as NameOperationStep,
	                ok1.Operation_Id as Operation_Id,
	                ok1.ProductSize_Id as ProductSize_Id,
	                ok1.Description as Note,
	                ok1.StandardHour as D1350,
	                ok2.StandardHour as D1700,
	                ok3.StandardHour as D2100,
	                ok4.StandardHour as D2500,
                    ok1.Description as 'Option',
                    ok1.DrawingPosition
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.OperationStep_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.Operation_Id
	                AND ok2.ProductSize_Id=6
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.Operation_Id 
	                AND ok3.ProductSize_Id=7
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.Operation_Id 
	                AND ok4.ProductSize_Id=3
	                AND ok1.Sequence=ok4.Sequence
                WHERE 
	                o.OperationNumber = 20 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 2 -- spatne id (1 => Vnejsi, 2 => Vnitrni)
	                AND ok1.ProductSize_Id = 5
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW  [dbo].[v_VTC_InnerVessel_OPSQ_40] AS
                SELECT
	                ok1.Sequence as Sequence,
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Name as NameOperationStep,
	                ok1.Operation_Id as Operation_Id,
	                ok1.ProductSize_Id as ProductSize_Id,
	                ok1.Description as Note,
	                ok1.StandardHour as D1350,
	                ok2.StandardHour as D1700,
	                ok3.StandardHour as D2100,
	                ok4.StandardHour as D2500,
                    ok1.Description as 'Option',
                    ok1.DrawingPosition
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.OperationStep_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.Operation_Id
	                AND ok2.ProductSize_Id=6
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.Operation_Id 
	                AND ok3.ProductSize_Id=7
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.Operation_Id 
	                AND ok4.ProductSize_Id=3
	                AND ok1.Sequence=ok4.Sequence
                WHERE 
	                o.OperationNumber = 40 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 2 -- spatne id (1 => Vnejsi, 2 => Vnitrni)
	                AND ok1.ProductSize_Id = 5
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_OutherVessel_OPSQ_10] AS
                SELECT
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Sequence as Sequence,
	                ok1.Name as NameOperationStep,					-- 2024-02-14 RP
	                --ok1.ID_Operace as ID_Operace,					-- 2024-02-14 RP
	                --ok1.ID_VyrobekVelikost as ID_VyrobekVelikost, -- 2024-02-14 RP
	                ok1.Operation_Id as Operation_Id,
	                ok1.StandardHour as D1800,
	                ok2.StandardHour as D2100,
	                ok3.StandardHour as D2500,
	                ok4.StandardHour as D3000,
	                ok1.Description as 'Option'						-- 2024-02-14 RP
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.ProductSize_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.[Operation_Id]
	                AND ok2.ProductSize_Id=2
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.[Operation_Id] --Asi chybny zaznam u D2100-ID6 -> 88 ale excel 85
	                AND ok3.ProductSize_Id=3
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.[Operation_Id] 
	                AND ok4.ProductSize_Id=4
	                AND ok1.Sequence=ok4.Sequence
                WHERE 
	                o.OperationNumber = 10 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 1
	                AND ok1.ProductSize_Id = 1
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_OutherVessel_OPSQ_20] AS
                SELECT
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Sequence as Sequence,
	                ok1.Name as NameOperationStep,					-- 2024-02-14 RP
	                --ok1.ID_Operace as ID_Operace,					-- 2024-02-14 RP
	                --ok1.ID_VyrobekVelikost as ID_VyrobekVelikost, -- 2024-02-14 RP
	                ok1.Operation_Id as Operation_Id,
	                ok1.StandardHour as D1800,
	                ok2.StandardHour as D2100,
	                ok3.StandardHour as D2500,
	                ok4.StandardHour as D3000,
	                ok1.Description as 'Option',						-- 2024-02-14 RP
                    ok1.DrawingPosition
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.ProductSize_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.[Operation_Id]
	                AND ok2.ProductSize_Id=2
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.[Operation_Id] --Asi chybny zaznam u D2100-ID6 -> 88 ale excel 85
	                AND ok3.ProductSize_Id=3
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.[Operation_Id] 
	                AND ok4.ProductSize_Id=4
	                AND ok1.Sequence=ok4.Sequence
                WHERE 
	                o.OperationNumber = 20 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 1
	                AND ok1.ProductSize_Id = 1
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_OutherVessel_OPSQ_40] AS
                SELECT
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Sequence as Sequence,
	                ok1.Name as NameOperationStep,					-- 2024-02-14 RP
	                --ok1.ID_Operace as ID_Operace,					-- 2024-02-14 RP
	                --ok1.ID_VyrobekVelikost as ID_VyrobekVelikost, -- 2024-02-14 RP
	                ok1.Operation_Id as Operation_Id,
	                ok1.StandardHour as D1800,
	                ok2.StandardHour as D2100,
	                ok3.StandardHour as D2500,
	                ok4.StandardHour as D3000,
	                ok1.Description as 'Option',						-- 2024-02-14 RP
                    ok1.DrawingPosition 
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.ProductSize_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.[Operation_Id]
	                AND ok2.ProductSize_Id=2
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.[Operation_Id] --Asi chybny zaznam u D2100-ID6 -> 88 ale excel 85
	                AND ok3.ProductSize_Id=3
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.[Operation_Id] 
	                AND ok4.ProductSize_Id=4
	                AND ok1.Sequence=ok4.Sequence
                WHERE 
	                o.OperationNumber = 40 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 1
	                AND ok1.ProductSize_Id = 1
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_Pipe_OPSQ_20] AS
                SELECT
	                ok1.OperationStep_Id as ID,
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Name as NameOperationStep,
	                ok1.Operation_Id as Operation_Id,
	                ok1.ProductSize_Id as ProductSize_Id,
	                ok1.Description as Note,
	                ok1.StandardHour as D1800_VT3,
	                ok2.StandardHour as D1800_VT6,
	                ok3.StandardHour as D1800_VT9,
	                ok4.StandardHour as D2100_VT11,
	                ok5.StandardHour as D2100_VT16,
	                ok6.StandardHour as D2100_VT21,
	                ok7.StandardHour as D2100_VT25,
	                ok8.StandardHour as D2500_VT26,
	                ok9.StandardHour as D2500_VT31,
	                ok10.StandardHour as D2500_VT43,
	                ok11.StandardHour as D3000_VT41,
	                ok12.StandardHour as D3000_VT50,
	                ok13.StandardHour as D3000_VT60,
                    ok2.Diameter,
                    ok2.DrawingPosition,
                    ok2.PipeBending,
	                ok1.Sequence
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.ProductSize_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.Operation_Id 
	                AND ok2.ProductSize_Id=9
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.Operation_Id 
	                AND ok3.ProductSize_Id=10
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.Operation_Id 
	                AND ok4.ProductSize_Id=11
	                AND ok1.Sequence=ok4.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok5 on o.Operation_Id = ok5.Operation_Id 
	                AND ok5.ProductSize_Id=12
	                AND ok1.Sequence=ok5.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok6 on o.Operation_Id = ok6.Operation_Id 
	                AND ok6.ProductSize_Id=13
	                AND ok1.Sequence=ok6.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok7 on o.Operation_Id = ok7.Operation_Id 
	                AND ok7.ProductSize_Id=14
	                AND ok1.Sequence=ok7.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok8 on o.Operation_Id = ok8.Operation_Id 
	                AND ok8.ProductSize_Id=15
	                AND ok1.Sequence=ok8.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok9 on o.Operation_Id = ok9.Operation_Id 
	                AND ok9.ProductSize_Id=16
	                AND ok1.Sequence=ok9.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok10 on o.Operation_Id = ok10.Operation_Id 
	                AND ok10.ProductSize_Id=17
	                AND ok1.Sequence=ok10.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok11 on o.Operation_Id = ok11.Operation_Id 
	                AND ok11.ProductSize_Id=18
	                AND ok1.Sequence=ok11.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok12 on o.Operation_Id = ok12.Operation_Id 
	                AND ok12.ProductSize_Id=19
	                AND ok1.Sequence=ok12.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok13 on o.Operation_Id = ok13.Operation_Id 
	                AND ok13.ProductSize_Id=20
	                AND ok1.Sequence=ok13.Sequence
                WHERE 
	                o.OperationNumber = 20 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 3
	                AND ok1.ProductSize_Id = 8
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_VTC_Pipe_OPSQ_30] AS
                SELECT
	                ok1.OperationStep_Id as ID,
	                vr.Name AS ProductLine,
	                vt.Name AS ProductType, 
	                ok1.Name as NameOperationStep,
	                ok1.Operation_Id as Operation_Id,
	                ok1.ProductSize_Id as ProductSize_Id,
	                ok1.Description as Note,
	                ok1.StandardHour as D1800_VT3,
	                ok2.StandardHour as D1800_VT6,
	                ok3.StandardHour as D1800_VT9,
	                ok4.StandardHour as D2100_VT11,
	                ok5.StandardHour as D2100_VT16,
	                ok6.StandardHour as D2100_VT21,
	                ok7.StandardHour as D2100_VT25,
	                ok8.StandardHour as D2500_VT26,
	                ok9.StandardHour as D2500_VT31,
	                ok10.StandardHour as D2500_VT43,
	                ok11.StandardHour as D3000_VT41,
	                ok12.StandardHour as D3000_VT50,
	                ok13.StandardHour as D3000_VT60,
                    ok2.Diameter,
                    ok2.DrawingPosition,
                    ok2.PipeBending,
                    ok1.[Sequence]
                FROM [dbo].[Operation] AS o 
                LEFT JOIN [dbo].[ProductLine] AS vr on o.ProductLine_Id = vr.ProductLine_Id
                LEFT JOIN [dbo].[ProductType] AS vt on o.ProductType_Id = vt.ProductType_Id
                LEFT JOIN [dbo].[OperationStep] AS ok1 on o.Operation_Id = ok1.Operation_Id
                LEFT JOIN [dbo].[ProductSize] AS vv on ok1.ProductSize_Id = vv.ProductSize_Id
                LEFT JOIN [dbo].[OperationStep] AS ok2 on o.Operation_Id = ok2.Operation_Id 
	                AND ok2.ProductSize_Id=9
	                AND ok1.Sequence=ok2.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok3 on o.Operation_Id = ok3.Operation_Id 
	                AND ok3.ProductSize_Id=10
	                AND ok1.Sequence=ok3.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok4 on o.Operation_Id = ok4.Operation_Id 
	                AND ok4.ProductSize_Id=11
	                AND ok1.Sequence=ok4.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok5 on o.Operation_Id = ok5.Operation_Id 
	                AND ok5.ProductSize_Id=12
	                AND ok1.Sequence=ok5.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok6 on o.Operation_Id = ok6.Operation_Id 
	                AND ok6.ProductSize_Id=13
	                AND ok1.Sequence=ok6.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok7 on o.Operation_Id = ok7.Operation_Id 
	                AND ok7.ProductSize_Id=14
	                AND ok1.Sequence=ok7.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok8 on o.Operation_Id = ok8.Operation_Id 
	                AND ok8.ProductSize_Id=15
	                AND ok1.Sequence=ok8.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok9 on o.Operation_Id = ok9.Operation_Id 
	                AND ok9.ProductSize_Id=16
	                AND ok1.Sequence=ok9.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok10 on o.Operation_Id = ok10.Operation_Id 
	                AND ok10.ProductSize_Id=17
	                AND ok1.Sequence=ok10.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok11 on o.Operation_Id = ok11.Operation_Id 
	                AND ok11.ProductSize_Id=18
	                AND ok1.Sequence=ok11.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok12 on o.Operation_Id = ok12.Operation_Id 
	                AND ok12.ProductSize_Id=19
	                AND ok1.Sequence=ok12.Sequence
                LEFT JOIN [dbo].[OperationStep] AS ok13 on o.Operation_Id = ok13.Operation_Id 
	                AND ok13.ProductSize_Id=20
	                AND ok1.Sequence=ok13.Sequence
                WHERE 
	                o.OperationNumber = 30 
	                AND vr.ProductLine_Id = 1 
	                AND vt.ProductType_Id = 3
	                AND ok1.ProductSize_Id = 8
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE   VIEW [dbo].[v_VTC_Pipe_20_30] as
                SELECT
                    view1.ID as 'ID',
                    view1.ProductLine as 'ProductLine',
                    view1.ProductType as 'ProductType',
                    view1.ProductSize_Id as 'ProductSizeId',
                    view1.NameOperationStep as 'NameOperationStep',
                    view1.[Sequence],
                    view2.Diameter,
                    view2.DrawingPosition,
                    view2.PipeBending,
                    view1.Operation_Id as 'OperationId_20',
                    view1.Note as 'Note_20',
                    view1.D1800_VT3 as 'D1800_VT3_20',
                    view1.D1800_VT6 as 'D1800_VT6_20',
                    view1.D1800_VT9 as 'D1800_VT9_20',
                    view1.D2100_VT11 as 'D2100_VT11_20',
                    view1.D2100_VT16 as 'D2100_VT16_20',
                    view1.D2100_VT21 as 'D2100_VT21_20',
                    view1.D2100_VT25 as 'D2100_VT25_20',
                    view1.D2500_VT26 as 'D2500_VT26_20',
                    view1.D2500_VT31 as 'D2500_VT31_20',
                    view1.D2500_VT43 as 'D2500_VT43_20',
                    view1.D3000_VT41 as 'D3000_VT41_20',
                    view1.D3000_VT50 as 'D3000_VT50_20',
                    view1.D3000_VT60 as 'D3000_VT60_20',
                    view2.Operation_Id as 'OperationId_30',
                    view2.Note as 'Note_30',
                    view2.D1800_VT3 as 'D1800_VT3_30',
                    view2.D1800_VT6 as 'D1800_VT6_30',
                    view2.D1800_VT9 as 'D1800_VT9_30',
                    view2.D2100_VT11 as 'D2100_VT11_30',
                    view2.D2100_VT16 as 'D2100_VT16_30',
                    view2.D2100_VT21 as 'D2100_VT21_30',
                    view2.D2100_VT25 as 'D2100_VT25_30',
                    view2.D2500_VT26 as 'D2500_VT26_30',
                    view2.D2500_VT31 as 'D2500_VT31_30',
                    view2.D2500_VT43 as 'D2500_VT43_30',
                    view2.D3000_VT41 as 'D3000_VT41_30',
                    view2.D3000_VT50 as 'D3000_VT50_30',
                    view2.D3000_VT60 as 'D3000_VT60_30'
                FROM [dbo].[v_VTC_Pipe_OPSQ_20] AS view1
                INNER JOIN [dbo].[v_VTC_Pipe_OPSQ_30] AS view2 
                ON view1.Sequence = view2.Sequence;
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[V_UsersRoles]
                AS
                SELECT u.DisplayName, u.Email, u.UserName, r.RoleId, r.Name AS Role, u.UserId
                FROM   cfg.Users AS u LEFT OUTER JOIN
                             cfg.UserRoles AS ur ON u.UserId = ur.UserId LEFT OUTER JOIN
                             cfg.Roles AS r ON ur.RoleId = r.RoleId
                WHERE (u.IsActive = 1) AND (r.IsActive = 1 OR
                             r.IsActive IS NULL)
                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationStep");

            migrationBuilder.DropTable(
                name: "Preparations");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "cfg");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "cfg");

            migrationBuilder.DropTable(
                name: "V_Preparation");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "PreparationType");

            migrationBuilder.DropTable(
                name: "ProductSize");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "cfg");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "cfg");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "cfg");

            migrationBuilder.DropTable(
                name: "ProductLine");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
