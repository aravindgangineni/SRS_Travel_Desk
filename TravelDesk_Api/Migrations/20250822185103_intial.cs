using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "HR Travel Admin" },
                    { 3, "Employee" },
                    { 4, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Department", "Email", "EmployeeId", "FirstName", "LastName", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, "IT", "admin@traveldesk.com", "A001", "System", "Admin", new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 }, 1 },
                    { 2, "HR", "hr@traveldesk.com", "T001", "Hr", "Travel", new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 }, 2 },
                    { 3, "Sales", "employee@traveldesk.com", "E001", "Regular", "Employee", new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 }, 3 },
                    { 4, "Engineering", "manager@traveldesk.com", "M001", "Project", "Manager", new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 }, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
