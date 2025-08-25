using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrectPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: new byte[] { 36, 50, 97, 36, 49, 49, 36, 57, 82, 46, 98, 65, 46, 110, 49, 74, 48, 106, 53, 66, 53, 108, 54, 102, 52, 71, 46, 109, 79, 57, 108, 48, 112, 48, 118, 49, 116, 50, 117, 51, 120, 52, 121, 53, 122, 54, 97, 55, 98, 56, 99, 57, 100, 48, 101, 49, 102 });
        }
    }
}
