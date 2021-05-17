using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    public partial class checkin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDateTime",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CheckInStatus",
                table: "Reservation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation",
                columns: new[] { "WorkstationId", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CheckInDateTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CheckInStatus",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation",
                columns: new[] { "WorkstationId", "Date" },
                unique: true);
        }
    }
}
