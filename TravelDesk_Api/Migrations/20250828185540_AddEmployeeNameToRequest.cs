using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeNameToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "TravelRequests");
        }
    }
}
