using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeminjamanRuangan.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonToBookingStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BookingStatusHistories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BookingStatusHistories");
        }
    }
}
