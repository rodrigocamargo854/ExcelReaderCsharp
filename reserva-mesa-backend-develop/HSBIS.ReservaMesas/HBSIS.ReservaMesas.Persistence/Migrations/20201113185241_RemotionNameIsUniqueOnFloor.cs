using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace HBSIS.ReservaMesas.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RemotionNameIsUniqueOnFloor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Floors_Name",
                table: "Floors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Floors_Name",
                table: "Floors",
                column: "Name",
                unique: true);
        }
    }
}
