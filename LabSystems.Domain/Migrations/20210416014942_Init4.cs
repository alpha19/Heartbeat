using Microsoft.EntityFrameworkCore.Migrations;

namespace LabSystems.Domain.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hmm",
                table: "LabSystems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hmm",
                table: "LabSystems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
