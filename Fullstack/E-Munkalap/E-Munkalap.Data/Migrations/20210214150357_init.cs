using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace E_Munkalap.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "munkalap_employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    AdLoginName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "munkalap_professions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "munkalap_roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "munkalap_sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SessionID = table.Column<string>(maxLength: 120, nullable: false),
                    AdLoginName = table.Column<string>(maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(maxLength: 60, nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    LastAccess = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "munkalap_employeeProfessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    ProfessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_employeeProfessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_munkalap_employeeProfessions_munkalap_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "munkalap_employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_munkalap_employeeProfessions_munkalap_professions_Profession~",
                        column: x => x.ProfessionId,
                        principalTable: "munkalap_professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Munkalap_Works",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RequesterName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    DeadLine = table.Column<DateTime>(nullable: true),
                    AssignDate = table.Column<DateTime>(nullable: true),
                    AssignDetails = table.Column<string>(nullable: true),
                    AssignerName = table.Column<string>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    FinishComment = table.Column<string>(nullable: true),
                    CheckDate = table.Column<DateTime>(nullable: true),
                    CheckerUser = table.Column<string>(nullable: true),
                    CheckComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Munkalap_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Munkalap_Works_munkalap_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "munkalap_employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Munkalap_Works_munkalap_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "munkalap_professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "munkalap_userRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AdLoginName = table.Column<string>(maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_munkalap_userRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_munkalap_userRoles_munkalap_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "munkalap_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "munkalap_employees",
                columns: new[] { "Id", "AdLoginName", "Name" },
                values: new object[,]
                {
                    { 1, "kiss.karoly@dolgozo.jedlik.eu", "Kiss Károly" },
                    { 2, null, "Ügyes Béla" },
                    { 3, null, "Mindenes Árpád" }
                });

            migrationBuilder.InsertData(
                table: "munkalap_professions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lakatos" },
                    { 2, "Asztalos" },
                    { 3, "Takarító" },
                    { 4, "Segédmunkás" },
                    { 5, "Kőműves" },
                    { 6, "Burkoló" }
                });

            migrationBuilder.InsertData(
                table: "munkalap_employeeProfessions",
                columns: new[] { "Id", "EmployeeId", "ProfessionId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 2, 1, 3 },
                    { 6, 2, 3 },
                    { 10, 3, 3 },
                    { 3, 1, 4 },
                    { 7, 2, 4 },
                    { 11, 3, 4 },
                    { 8, 2, 5 },
                    { 9, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_employeeProfessions_EmployeeId",
                table: "munkalap_employeeProfessions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_employeeProfessions_ProfessionId",
                table: "munkalap_employeeProfessions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_employees_Name",
                table: "munkalap_employees",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_professions_Name",
                table: "munkalap_professions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_roles_Name",
                table: "munkalap_roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_sessions_SessionID",
                table: "munkalap_sessions",
                column: "SessionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_munkalap_userRoles_RoleId",
                table: "munkalap_userRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Munkalap_Works_EmployeeId",
                table: "Munkalap_Works",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Munkalap_Works_ProfessionId",
                table: "Munkalap_Works",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "munkalap_employeeProfessions");

            migrationBuilder.DropTable(
                name: "munkalap_sessions");

            migrationBuilder.DropTable(
                name: "munkalap_userRoles");

            migrationBuilder.DropTable(
                name: "Munkalap_Works");

            migrationBuilder.DropTable(
                name: "munkalap_roles");

            migrationBuilder.DropTable(
                name: "munkalap_employees");

            migrationBuilder.DropTable(
                name: "munkalap_professions");
        }
    }
}
