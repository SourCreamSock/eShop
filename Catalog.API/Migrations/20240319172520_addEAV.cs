using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class addEAV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogItemAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItemAttributeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogItemAttributeId = table.Column<int>(type: "int", nullable: false),
                    CatalogItemCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemAttributeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItemAttributeCategories_CatalogItemAttributes_CatalogItemAttributeId",
                        column: x => x.CatalogItemAttributeId,
                        principalTable: "CatalogItemAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItemAttributeCategories_CatalogItemCategories_CatalogItemCategoryId",
                        column: x => x.CatalogItemCategoryId,
                        principalTable: "CatalogItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CatalogItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItemAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueNumber = table.Column<long>(type: "bigint", nullable: true),
                    ValueBool = table.Column<bool>(type: "bit", nullable: true),
                    ValueFloat = table.Column<double>(type: "float", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CatalogItemAttributeId = table.Column<int>(type: "int", nullable: false),
                    CatalogItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItemAttributeValues_CatalogItemAttributes_CatalogItemAttributeId",
                        column: x => x.CatalogItemAttributeId,
                        principalTable: "CatalogItemAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItemAttributeValues_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemAttributeCategories_CatalogItemAttributeId",
                table: "CatalogItemAttributeCategories",
                column: "CatalogItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemAttributeCategories_CatalogItemCategoryId",
                table: "CatalogItemAttributeCategories",
                column: "CatalogItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemAttributeValues_CatalogItemAttributeId",
                table: "CatalogItemAttributeValues",
                column: "CatalogItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemAttributeValues_CatalogItemId",
                table: "CatalogItemAttributeValues",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CategoryId",
                table: "CatalogItems",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItemAttributeCategories");

            migrationBuilder.DropTable(
                name: "CatalogItemAttributeValues");

            migrationBuilder.DropTable(
                name: "CatalogItemAttributes");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CatalogItemCategories");
        }
    }
}
