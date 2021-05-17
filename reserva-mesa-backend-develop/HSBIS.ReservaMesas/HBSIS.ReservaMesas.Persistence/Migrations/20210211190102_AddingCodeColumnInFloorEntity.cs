using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    public partial class AddingCodeColumnInFloorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Floors",
                nullable: false,
                defaultValue: "");

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
                name: "Code",
                table: "Floors");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkstationId_Date",
                table: "Reservation",
                columns: new[] { "WorkstationId", "Date" });
        }
    }
}
