﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    public partial class userName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reservation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation",
                columns: new[] { "WorkstationId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation",
                columns: new[] { "WorkstationId", "Date" });
        }
    }
}
