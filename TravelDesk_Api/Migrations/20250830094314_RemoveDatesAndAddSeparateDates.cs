using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDatesAndAddSeparateDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dates",
                table: "TravelRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dates",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
