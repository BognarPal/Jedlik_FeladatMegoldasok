using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DolgozoNyilvantartas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SzervezetiEgysegek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nev = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzervezetiEgysegek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dolgozok",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdoazonositoJel = table.Column<string>(maxLength: 10, nullable: false),
                    Nev = table.Column<string>(maxLength: 150, nullable: false),
                    EvesSzabadsag = table.Column<int>(nullable: false),
                    SzervezetiEgysegId = table.Column<int>(nullable: false),
                    Beosztas = table.Column<string>(maxLength: 100, nullable: true),
                    FelvetelDatuma = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolgozok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dolgozok_SzervezetiEgysegek_SzervezetiEgysegId",
                        column: x => x.SzervezetiEgysegId,
                        principalTable: "SzervezetiEgysegek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SzervezetiEgysegek",
                columns: new[] { "Id", "Nev" },
                values: new object[] { 1, "Igazgatóság" });

            migrationBuilder.InsertData(
                table: "SzervezetiEgysegek",
                columns: new[] { "Id", "Nev" },
                values: new object[] { 2, "Termelés" });

            migrationBuilder.InsertData(
                table: "SzervezetiEgysegek",
                columns: new[] { "Id", "Nev" },
                values: new object[] { 3, "Marketing" });

            migrationBuilder.CreateIndex(
                name: "IX_Dolgozok_AdoazonositoJel",
                table: "Dolgozok",
                column: "AdoazonositoJel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dolgozok_SzervezetiEgysegId",
                table: "Dolgozok",
                column: "SzervezetiEgysegId");

            migrationBuilder.CreateIndex(
                name: "IX_SzervezetiEgysegek_Nev",
                table: "SzervezetiEgysegek",
                column: "Nev",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dolgozok");

            migrationBuilder.DropTable(
                name: "SzervezetiEgysegek");
        }
    }
}
