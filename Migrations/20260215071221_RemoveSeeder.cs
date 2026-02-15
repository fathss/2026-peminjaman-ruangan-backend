using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PeminjamanRuangan.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingStatusHistories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingStatusHistories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_BookingStatusHistories_ChangedByUserId",
                table: "BookingStatusHistories",
                column: "ChangedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingStatusHistories_Users_ChangedByUserId",
                table: "BookingStatusHistories",
                column: "ChangedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingStatusHistories_Users_ChangedByUserId",
                table: "BookingStatusHistories");

            migrationBuilder.DropIndex(
                name: "IX_BookingStatusHistories_ChangedByUserId",
                table: "BookingStatusHistories");

            migrationBuilder.InsertData(
                table: "RoomBookings",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsDeleted", "Purpose", "RoomId", "StartTime", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Diskusi Tugas Akhir", 1, new DateTime(2024, 7, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Pending", null, 2 },
                    { 2, new DateTime(2026, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), false, "Seminar Proyek", 3, new DateTime(2024, 7, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), "Pending", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookingStatusHistories",
                columns: new[] { "Id", "ChangedAt", "ChangedByUserId", "DeletedAt", "IsDeleted", "NewStatus", "OldStatus", "RoomBookingId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified), 2, null, false, "Pending", "Pending", 1 },
                    { 2, new DateTime(2026, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified), 2, null, false, "Pending", "Pending", 2 }
                });
        }
    }
}
