using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleServiceManagement.API.Migrations
{
    public partial class Vehiclestausadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleStatus",
                table: "Vehicles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleStatus",
                table: "Vehicles");
        }
    }
}
