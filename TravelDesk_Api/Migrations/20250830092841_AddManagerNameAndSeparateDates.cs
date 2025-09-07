using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerNameAndSeparateDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckinDate",
                table: "TravelRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FlightDate",
                table: "TravelRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckinDate",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "FlightDate",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "TravelRequests");
        }
    }
}
