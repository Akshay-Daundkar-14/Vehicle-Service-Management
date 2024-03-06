using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleServiceManagement.API.Migrations
{
    public partial class ServiceRecordItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "ServiceRecordItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ServiceRecordItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "ServiceRecordItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ServiceRecordItems");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ServiceRecordItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ServiceRecordItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
