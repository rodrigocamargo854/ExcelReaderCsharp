using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddIndexInNameOfWorkstation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Workstations_Name",
                table: "Workstations",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workstations_Name",
                table: "Workstations");
        }
    }
}
