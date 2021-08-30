using Microsoft.EntityFrameworkCore.Migrations;

namespace pizzeria.service.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "PasswordHash", "Phone" },
                values: new object[] { 1, "Employee", "admin@localhost.com", "admin", "AQAAAAEAACcQAAAAEAvf3h9tFg/+as1/hx10qBWjXg0tYGISsTyyKKTAlE4+9HkiI6BuF8mqLM7HsoE52g==", "112" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { 1, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
