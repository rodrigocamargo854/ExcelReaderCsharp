using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    public partial class AddCancellationDateOnReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledOn",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledOn",
                table: "Reservation");
        }
    }
}
