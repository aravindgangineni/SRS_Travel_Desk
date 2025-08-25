using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class UniquePasswordHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 66, 85, 111, 76, 53, 56, 82, 73, 88, 54, 105, 97, 67, 121, 74, 80, 57, 101, 112, 77, 65, 117, 82, 100, 104, 65, 66, 113, 109, 113, 54, 90, 117, 108, 98, 47, 90, 71, 78, 77, 100, 115, 69, 52, 81, 109, 46, 67, 54, 74, 54, 69, 75 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 69, 88, 54, 114, 113, 72, 57, 97, 118, 76, 73, 122, 98, 102, 122, 49, 109, 52, 120, 115, 82, 46, 112, 89, 75, 49, 88, 98, 121, 101, 55, 84, 88, 111, 85, 102, 72, 48, 84, 67, 80, 83, 102, 98, 75, 56, 120, 88, 86, 57, 99, 87, 97 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 81, 101, 116, 121, 104, 46, 73, 79, 86, 101, 78, 85, 103, 77, 78, 51, 80, 65, 83, 72, 68, 79, 48, 86, 50, 81, 57, 90, 69, 120, 70, 82, 76, 97, 51, 108, 111, 101, 50, 108, 66, 66, 88, 119, 113, 66, 67, 105, 53, 55, 65, 52, 50 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 121, 66, 78, 67, 88, 116, 51, 74, 108, 49, 69, 118, 98, 72, 110, 74, 56, 114, 104, 78, 49, 46, 117, 71, 49, 67, 113, 65, 86, 69, 48, 117, 47, 120, 121, 117, 73, 78, 79, 82, 71, 102, 102, 102, 50, 109, 74, 56, 108, 103, 119, 122, 121 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Department", "Email", "EmployeeId", "FirstName", "LastName", "ManagerId", "PasswordHash", "RoleId" },
                values: new object[] { 5, "Marketing", "janedoe@traveldesk.com", "E123", "Jane", "Doe", 4, new byte[] { 36, 50, 97, 36, 49, 49, 36, 81, 101, 116, 121, 104, 46, 73, 79, 86, 101, 78, 85, 103, 77, 78, 51, 80, 65, 83, 72, 68, 79, 48, 86, 50, 81, 57, 90, 69, 120, 70, 82, 76, 97, 51, 108, 111, 101, 50, 108, 66, 66, 88, 119, 113, 66, 67, 105, 53, 55, 65, 52, 50 }, 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 113, 83, 50, 113, 97, 108, 117, 89, 99, 106, 112, 111, 57, 65, 116, 78, 105, 49, 48, 104, 56, 117, 75, 57, 72, 118, 75, 106, 122, 102, 70, 66, 101, 66, 90, 50, 113, 120, 122, 103, 120, 56, 109, 73, 78, 117, 78, 101, 54, 121, 66, 120, 79 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 113, 83, 50, 113, 97, 108, 117, 89, 99, 106, 112, 111, 57, 65, 116, 78, 105, 49, 48, 104, 56, 117, 75, 57, 72, 118, 75, 106, 122, 102, 70, 66, 101, 66, 90, 50, 113, 120, 122, 103, 120, 56, 109, 73, 78, 117, 78, 101, 54, 121, 66, 120, 79 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 113, 83, 50, 113, 97, 108, 117, 89, 99, 106, 112, 111, 57, 65, 116, 78, 105, 49, 48, 104, 56, 117, 75, 57, 72, 118, 75, 106, 122, 102, 70, 66, 101, 66, 90, 50, 113, 120, 122, 103, 120, 56, 109, 73, 78, 117, 78, 101, 54, 121, 66, 120, 79 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 113, 83, 50, 113, 97, 108, 117, 89, 99, 106, 112, 111, 57, 65, 116, 78, 105, 49, 48, 104, 56, 117, 75, 57, 72, 118, 75, 106, 122, 102, 70, 66, 101, 66, 90, 50, 113, 120, 122, 103, 120, 56, 109, 73, 78, 117, 78, 101, 54, 121, 66, 120, 79 });
        }
    }
}
