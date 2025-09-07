using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketFileUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketFileUrl",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketFileUrl",
                table: "TravelRequests");
        }
    }
}
