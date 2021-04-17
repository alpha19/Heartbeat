using Microsoft.EntityFrameworkCore.Migrations;

namespace LabSystems.Domain.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hmm",
                table: "LabSystems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hmm",
                table: "LabSystems");
        }
    }
}
