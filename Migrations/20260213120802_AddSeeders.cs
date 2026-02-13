using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PeminjamanRuangan.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Description", "IsActive", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 30, "Ruang kelas", true, "Gedung D3, LT. 2", "HH-203" },
                    { 2, 30, "Ruang kelas", true, "Gedung SAW, LT. 6", "SAW-06.06" },
                    { 3, 120, "Ruang kelas", true, "Gedung SAW, LT. 7", "SAW-07.10" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "admin@campus.com", "$2a$11$ec/jgyW.C8Q61nprj0IvVesMTiFyg3eaF0hMc/eNSo1PaWIR6uPiC", "Admin", "admin" },
                    { 2, "user@campus.com", "$2a$11$Op6pjkCaU.9FkqNrtWkJ2OpWe/FE8f5urbgfPZhZRaP8d5743/eUu", "User", "user" }
                });

            migrationBuilder.InsertData(
                table: "RoomBookings",
                columns: new[] { "Id", "CreatedAt", "EndTime", "IsDeleted", "Purpose", "RoomId", "StartTime", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, "Diskusi Tugas Akhir", 1, new DateTime(2024, 7, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Pending", null, 2 },
                    { 2, new DateTime(2026, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), false, "Seminar Proyek", 3, new DateTime(2024, 7, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), "Pending", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookingStatusHistories",
                columns: new[] { "Id", "ChangedAt", "ChangedByUserId", "NewStatus", "OldStatus", "RoomBookingId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified), 2, "Pending", "Pending", 1 },
                    { 2, new DateTime(2026, 1, 1, 9, 5, 0, 0, DateTimeKind.Unspecified), 2, "Pending", "Pending", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
