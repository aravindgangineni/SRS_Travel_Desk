using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "TravelRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_ManagerId",
                table: "TravelRequests",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelRequests_Users_ManagerId",
                table: "TravelRequests",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_Users_ManagerId",
                table: "TravelRequests");

            migrationBuilder.DropIndex(
                name: "IX_TravelRequests_ManagerId",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "TravelRequests");
        }
    }
}
