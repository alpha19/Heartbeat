using Microsoft.EntityFrameworkCore.Migrations;

namespace LabSystems.Domain.Migrations
{
    public partial class Init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverProviderName",
                table: "DiskDrives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverVersion",
                table: "DiskDrives",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverProviderName",
                table: "DiskDrives");

            migrationBuilder.DropColumn(
                name: "DriverVersion",
                table: "DiskDrives");
        }
    }
}
